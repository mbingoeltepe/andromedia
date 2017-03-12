using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using System.Web.UI;
using System.Security.Principal;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Service;
using MovieLibrary.Service.IServices;
using MovieLibrary.Helpers;
using System.Net.Mail;
using System.Net;

namespace MovieLibrary.Controllers
{
    public class AdminController : BaseController
    {
        public readonly int pageSize = 10;

        private IMembershipService membershipService;
        private IInsertRequestService insertRequestService;
        private IMediaService mediaService;

        public AdminController()
        {
            membershipService = MembershipService.Instance;
            insertRequestService = InsertRequestService.Instance;
            mediaService = MediaService.Instance;
        }

        public AdminController(IMembershipService membershipService, IInsertRequestService insertRequestService, IMediaService mediaService)
        {
            this.membershipService = membershipService;
            this.insertRequestService = insertRequestService;
            this.mediaService = mediaService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ShowUsers()
        {

            IQueryable<User> user = MembershipService.Instance.GetAllUsers();
            
            return View("ShowUsers", user);
        }

        [Authorize]
        public PartialViewResult FilternUsers(FormCollection collection, IPrincipal loggedInUser)
        {
            string userName = collection["NameBox"];

            IQueryable<User> user = MembershipService.Instance.GetUserByUsername(userName, MembershipService.Instance.GetCurrentUser(loggedInUser.Identity.Name));

            return PartialView("../Admin/UserView", user);
        }


        [HttpPost, Authorize]
        public ActionResult UserLoeschen(int id, IPrincipal admin)
        {
            //// Quote vom User werden gelöscht
            //quoteService = AllQuotesService.Instance;

            //IQueryable<Quote>  quote = quoteService.GetQuoteByUserId(id);

            // foreach (var q in quote)
            //{
            //    quoteService.DeleteQuote(q);
            //}

            // // Medien vom User werden gelöscht 
            //userMediaService = UserMediaService.Instance;

            //IQueryable<UserMedia> medeien = userMediaService.GetUserMedienByUserId(id);

            //foreach (var m in medeien)
            //{
            //    userMediaService.DeleteMedien(m);
            //}

            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            // User wird gelöscht
           User user = MembershipService.Instance.GetUserByUserId(id);

           user.Closed = true;

           MembershipService.Instance.Save();

           String mailAddress = user.Username;

           try
           {
               MembershipService.Instance.renewUserVerificationCode(mailAddress);

               MailMessage msg = new MailMessage();
               MailAddress to = new MailAddress(mailAddress);
               MailAddress from = new MailAddress("andromedia@gmx.at");

               msg.To.Add(to);
               msg.From = from;
               msg.Subject = "Andromedia Account deaktiviert";
               msg.IsBodyHtml = true;
               msg.Body = "Es tut uns Leid, Ihnen mitteilen zu müssen, dass Ihr Andromedia - Account deaktiviert wurde.";

               SmtpClient smtp = new SmtpClient("mail.gmx.net");

               smtp.Credentials = new NetworkCredential("andromedia@gmx.at", "andro20media11");

               try
               {
                   smtp.Send(msg);

                   ViewData["title"] = "User gelöscht - Andromedia";
                   ViewData["message"] = "Der User wurde erfolgreich gelöscht und per E-Mail davon untterrichtet.";
                   ViewData["link"] = Request.UrlReferrer.ToString();
                   ViewData["linkText"] = "Zurück";
                   return View("Message");
               }
               catch (Exception)
               {
                   ViewData["title"] = "Fehler - Andromedia";
                   ViewData["message"] = "Der User wurde gelöscht, jedoch beim Senden der Benachrichtigung ist ein Fehler aufgetreten.";
                   ViewData["link"] = Request.UrlReferrer.ToString();
                   ViewData["linkText"] = "Zurück";
                   return View("Message");
               }
           }

           catch (Exception e)
           {
               ViewData["title"] = "Fehler - Andromedia";
               ViewData["message"] = e.Message;
               ViewData["link"] = Request.UrlReferrer.ToString();
               ViewData["linkText"] = "Zurück";
               return View("Message");
           }
           
            // return ShowUsers(); 
                
        }

        [HttpGet, Authorize]
        public ActionResult MediaRequests(IPrincipal admin, int page = 0)
        {
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            IQueryable<InsertRequest> insertRequests = insertRequestService.GetAllOrderedByRequestDate();
            PaginatedList<InsertRequest> paginatedInsertRequests = new PaginatedList<InsertRequest>(insertRequests, page, pageSize);

            return View("MediaRequests", paginatedInsertRequests);
        }

        [HttpPost, Authorize]
        public ActionResult ConfirmRequestBook(int id, FormCollection formCollection, IPrincipal admin)
        {
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            InsertRequest insertRequest = insertRequestService.GetById(id);

            if (insertRequest == null)
                return View("NotFound");

            Book book = insertRequest.Media as Book;

            if (book == null)
                return View("NotFound");

            if (TryUpdateModel<Book>(book))
            {
                book.Pending = false;
                insertRequestService.Delete(insertRequest);
                insertRequestService.Save();
                return RedirectToAction("MediaRequests");
            }

            return View("MediaRequests", CreateErrorRequest(insertRequest));
        }

        [HttpPost, Authorize]
        public ActionResult ConfirmRequestMovie(int id, FormCollection formCollection, IPrincipal admin)
        {
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            InsertRequest insertRequest = insertRequestService.GetById(id);

            if (insertRequest == null)
                return View("NotFound");

            Movie movie = insertRequest.Media as Movie;

            if (movie == null)
                return View("NotFound");

            if (TryUpdateModel<Movie>(movie))
            {
                movie.Pending = false;
                insertRequestService.Delete(insertRequest);
                insertRequestService.Save();
                return RedirectToAction("MediaRequests");
            }

            return View("MediaRequests", CreateErrorRequest(insertRequest));
        }

        [HttpPost, Authorize]
        public ActionResult ConfirmRequestTVShow(int id, FormCollection formCollection, IPrincipal admin)
        {
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            InsertRequest insertRequest = insertRequestService.GetById(id);

            if (insertRequest == null)
                return View("NotFound");

            TV_Show tv_show = insertRequest.Media as TV_Show;

            if (tv_show == null)
                return View("NotFound");

            int seasonsForm = 0;

            try
            {
                seasonsForm = Convert.ToInt32(formCollection["Seasons"]);
            }
            catch (Exception)
            {
                ViewData.ModelState.AddModelError("Seasons", "");
            }

            if (seasonsForm > 100)
            {
                seasonsForm = 0;
                ViewData.ModelState.AddModelError("Seasons", "");
            }

            if (TryUpdateModel<TV_Show>(tv_show))
            {
                if (seasonsForm > 0)
                {
                    UpdateSeasons(tv_show, seasonsForm - tv_show.Season.Count);
                    tv_show.Pending = false;
                    insertRequestService.Delete(insertRequest);
                    insertRequestService.Save();
                    return RedirectToAction("MediaRequests");
                }
            }

            return View("MediaRequests", CreateErrorRequest(insertRequest));
        }

        [HttpGet, Authorize]
        public ActionResult DeclineRequest(int id, IPrincipal admin)
        {
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            InsertRequest insertRequest = insertRequestService.GetById(id);

            if (insertRequest == null)
                return View("NotFound");

            insertRequestService.Delete(insertRequest);
            insertRequestService.Save();

            return RedirectToAction("MediaRequests");
        }

        private void UpdateSeasons(TV_Show tv_show, int diff)
        {
            if(diff == 0)
                return;

            IQueryable<Season> seasons = from season in tv_show.Season.AsQueryable<Season>()
                                         orderby season.Number
                                         select season;

            if(diff < 0)
            {
                IQueryable<Season> newSeason = seasons.Skip<Season>(tv_show.Season.Count + diff);

                foreach(Season season in newSeason)
                {
                    mediaService.DeleteSeason(season);
                }
            }

            if(diff > 0)
            {
                int last = seasons.Last<Season>().Number;

                int count = seasons.Count<Season>() + diff;

                for(last++; last <= count; last++)
                {
                    Season s = new Season();
                    s.Number = last;
                    s.TV_Show = tv_show;
                    tv_show.Season.Add(s);
                }
            }
        }

        private PaginatedList<InsertRequest> CreateErrorRequest(InsertRequest insertRequest)
        {
            List<InsertRequest> insertRequests = new List<InsertRequest>();
            insertRequests.Add(insertRequest);
            return new PaginatedList<InsertRequest>(insertRequests.AsQueryable<InsertRequest>(), 0, pageSize);
        }
    }
}