using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Daos;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Service.IServices;
using MovieLibrary.Daos.EntityFramework;
using System.Security.Principal;

namespace MovieLibrary.Controllers
{
    public class QuoteController : BaseController
    {
        public readonly int pageSize = 10;

        private IQuoteService<Quote> quoteService;
        private IMembershipService membershipService;


        public QuoteController()
        {
            this.membershipService = MembershipService.Instance;
            this.quoteService = AllQuotesService.Instance;
        }

        public QuoteController(IQuoteService<Quote> quoteService)
        {
            this.quoteService = quoteService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TopQuotes(int page = 0)
        {
            IQueryable<Quote> quotes = quoteService.GetAllQuotes().OrderByDescending(q => q.Invocations);

            PaginatedTopQuotesList paginatedQuotes = new PaginatedTopQuotesList(quotes, page, pageSize);
 
            return View("TopQuotes", paginatedQuotes);
        }

        [HttpGet]
        public ActionResult MediaDetails(int mediaId, string mediaType)
        {
            if (typeof(Movie).Name == mediaType)
            {
                Movie movie = MediaService.Instance.GetMovieById(mediaId);
                return RedirectToAction("Movie", "Media", new { id = movie.Id });
            }
            else if (typeof(Book).Name == mediaType)
            {
                Book book = MediaService.Instance.GetBookById(mediaId);
                return RedirectToAction("Book", "Media", new { id = book.Id });
            }
            else if (typeof(TV_Show).Name == mediaType)
            {
                TV_Show tv_show = MediaService.Instance.GetTvShowById(mediaId);
                return RedirectToAction("TVShow", "Media", new { id = tv_show.Id });
            }
            else
                throw new ArgumentException();
        }

        public ActionResult ShowQuotesMedia(int mediaId)
        {
            var resultMedia = MediaService.Instance.GetMediaById(mediaId);

            // Is a schaß so
            //List<Quote> quotes = new List<Quote>();
            //foreach (var quote in resultMedia.Quote)
            //{
            //    // Wenn Ranking des Zitates großer null ist. Wird das Zitat angezeigt                             
                
            //    if (quote.Ranking > 0)
            //    {
            //        quotes.Add(quote);
            //    }
            
            //}

            return View("ShowQuotesMedia", resultMedia);
        }


        public ActionResult AddQuote(int id, FormCollection collection, IPrincipal iuser)
        {

            User user = MembershipDao.Instance.GetCurrentUser(iuser.Identity.Name);

            Book book = MediaService.Instance.GetBookById(id);

            if (book != null)
            {
                QuoteBook quoteBook = new QuoteBook();

                quoteBook.Language = collection["Language"];
                quoteBook.Character = collection["Character"];
                quoteBook.QuoteString = collection["QuoteString"];
                quoteBook.MediaId = id;


                quoteBook.UserId = user.Id;
                quoteBook.Book = book;

                quoteService.AddQuote(quoteBook);

            }

            Movie movie = MediaService.Instance.GetMovieById(id);

            if (movie != null)
            {
                QuoteMovie quoteMovie = new QuoteMovie();

                quoteMovie.Language = collection["Language"];
                quoteMovie.Character = collection["Character"];
                quoteMovie.OccurenceTime = collection["Wann"];
                quoteMovie.QuoteString = collection["QuoteString"];
                quoteMovie.MediaId = id;


                quoteMovie.UserId = user.Id;
                quoteMovie.Movie = movie;

                quoteService.AddQuote(quoteMovie);

            }

            TV_Show tvshow = MediaService.Instance.GetTvShowById(id);

            if (tvshow != null)
            {
                QuoteTV_Show quoteTVShow = new QuoteTV_Show();


                quoteTVShow.Language = collection["Language"];
                quoteTVShow.Character = collection["Character"];
                quoteTVShow.OccurenceTime = collection["Wann"];
                quoteTVShow.QuoteString = collection["QuoteString"];

                quoteTVShow.MediaId = id;
                quoteTVShow.UserId = user.Id;

                Season season = MediaService.Instance.GetSeasonById(tvshow.Season.ElementAt<Season>(0).Id);

                Episode episode = new Episode();

                episode.Name = season.Episode.ElementAt<Episode>(0).Name;
                episode.Number = season.Episode.ElementAt<Episode>(0).Number;
                episode.Season = season;

                quoteTVShow.Episode = episode;
                

                quoteService.AddQuote(quoteTVShow);

            }


            //return View("ShowQuotesMedia", quotes);
            //return RedirectToAction("ShowQuotesMedia", "Quote", new { mediaId = id });
            return View("AddQuoteMessage");
        }

        [Authorize]
        public ActionResult ShowNotRankingZitate()
        {
            IQueryable<Quote> allQuotes = quoteService.GetAllNotRankingQuotes("", "", "");

            return View("ShowAllNotRankingQuotes", allQuotes);
        }

        [Authorize]
        public PartialViewResult FilternQuotes(FormCollection collection)
        {
            string userName = collection["NotRankingUserList"];
            string language = collection["NotRankingLanguage"];
            string title = collection["NotRankingMediumTitle"];

            IQueryable<Quote> quotes = quoteService.GetAllNotRankingQuotes(userName,language,title);


            return PartialView("../Quote/NotRankingQuotesViews", quotes);
        }

        [HttpPost, Authorize]
        public ActionResult ZitatRanken(int id, FormCollection collection ,IPrincipal admin)
        {
            int ranken = Convert.ToInt32(collection["RankenWert"]);
            
            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");

            if ((collection["Rolle"] == "") ||
                    (collection["QuoteString"] == ""))
                return View("SaveFailure");

            // Zitat wird gerankt
            Quote quote = quoteService.GetQuoteById(id);

            quote.Ranking = ranken;
            quote.Character = collection["Rolle"];
            quote.OccurenceTime = collection["Wann"];
            quote.Language = collection["Language"];
            quote.QuoteString = collection["QuoteString"];
            quoteService.SaveQuote();

            return ShowNotRankingZitate();

        }

        [HttpPost, Authorize]
        public ActionResult ZitatLoeschen(int id, IPrincipal admin)
        {

            if (!membershipService.IsAdmin(admin.Identity.Name))
                return View("NotAuthorized");


            // Zitat wird gelöscht
            Quote quote = quoteService.GetQuoteById(id);

            quoteService.DeleteQuote(quote);

            return ShowNotRankingZitate();

        }


    }
}
