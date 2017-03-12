using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Service.ServicesImpl;

namespace MovieLibrary.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected override void  OnActionExecuting(ActionExecutingContext filterContext)
        {
            var list = Enum.GetValues(typeof(Models.MediaTypeEnum));
            List<string> mediaTypeList = new List<string>();
            foreach (var i in list)
            {
                if (i.Equals(MediaTypeEnum.Quotes))
                {
                    continue;
                }
                if (i.Equals(MediaTypeEnum.AllMedia))
                {
                    mediaTypeList.Add("Alle Medien");
                }
                else if (i.Equals(MediaTypeEnum.TVShows))
                {
                    mediaTypeList.Add("TV Serien");
                }
                else if (i.Equals(MediaTypeEnum.Books))
                {
                    mediaTypeList.Add("Bücher");
                }
                else if (i.Equals(MediaTypeEnum.Movies))
                {
                    mediaTypeList.Add("Filme");
                }
                else
                {
                    mediaTypeList.Add(i.ToString());
                }
            }

            ViewData["Types"] = new SelectList(mediaTypeList);

            var list1 = Enum.GetValues(typeof(Models.MediaAufberwahrungsortEnum));
            List<string> mediaStoragePlaceList = new List<string>();
            foreach (var i in list1)
            {

                mediaStoragePlaceList.Add(i.ToString());
       
            }

            ViewData["Storage"] = new SelectList(mediaStoragePlaceList);

            var list2 = Enum.GetValues(typeof(Models.UserMediaStatusEnum));
            List<string> mediaStatusList = new List<string>();
            foreach (var i in list2)
            {

                mediaStatusList.Add(i.ToString());

            }
            
            ViewData["Status"] = new SelectList(mediaStatusList);

            var list3 = Enum.GetValues(typeof(Models.UserMediaStorageDeviceEnum));
            List<string> mediaStorageDeviceList = new List<string>();
            foreach (var i in list3)
            {

                mediaStorageDeviceList.Add(i.ToString());

            }
            
            ViewData["Device"] = new SelectList(mediaStorageDeviceList);

            var list4 = Enum.GetValues(typeof(Models.QuoteLanguageEnum));
            List<string> quoteLanguageList = new List<string>();
            foreach (var i in list4)
            {

                quoteLanguageList.Add(i.ToString());

            }

            ViewData["Language"] = new SelectList(quoteLanguageList);


            var genreArray = Enum.GetValues(typeof(Models.GenreEnum));
            ViewData["Genre"] = new SelectList(genreArray);

            List<int> seasons = new List<int>();
            for (int i = 1; i <= 100; i++)
            {
                seasons.Add(i);
            }

            ViewData["Seasons"] = new SelectList(seasons);

            // Anfang: Hier wird eine Liste mit UserName von Zitat hinzugefügte(ranking=0) Usern genomen
            IQueryable<Quote> quotes = AllQuotesService.Instance.GetAllNotRankingQuotes("","","");
            List<string> userList = new List<string>();
            userList.Add("");
            foreach (var i in quotes)
            {
                for (int j = 0; j <= userList.Count; j++)
                {
                    if (!userList.Contains(i.User.Username)) 
                          userList.Add(i.User.Username);

                }

            }

            ViewData["NotRankingUserList"] = new SelectList(userList);
            // Ende: 

            // Anfang: Hier wird eine Liste mit Sprache von Zitat hinzugefügte(ranking=0) Usern genomen
            IQueryable<Quote> quotesLanguage = AllQuotesService.Instance.GetAllNotRankingQuotes("", "", "");
            List<string> userListLanguage = new List<string>();
            userListLanguage.Add("");

            foreach (var i in quotesLanguage)
            {
                for (int j = 0; j <= userList.Count; j++)
                {
                    if (!userListLanguage.Contains(i.Language))
                        userListLanguage.Add(i.Language);

                }

            }

            ViewData["NotRankingLanguage"] = new SelectList(userListLanguage);
            // Ende: 

            // Anfang: Hier wird eine Liste mit Title von Zitat hinzugefügte(ranking=0) Usern genomen 
            IQueryable<Quote> quotesTitle = AllQuotesService.Instance.GetAllNotRankingQuotes("", "", "");
            List<string> title = new List<string>();
            title.Add("");

            foreach (var i in quotesTitle)
            {
                for (int j = 0; j <= userList.Count; j++)
                {
                    if (!title.Contains(i.Media.Title))
                        title.Add(i.Media.Title);

                }

            }

            ViewData["NotRankingMediumTitle"] = new SelectList(title);
            // Ende:  

            List<int> ranken = new List<int>();

            for (int j = 1; j <= 3; j++)
            {
                ranken.Add(j);
            }

            ViewData["RankenWert"] = new SelectList(ranken);
        }


        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            //the RequireHttpsAttribute set on the Controller Action will handle redirecting to Https. 
            // We just need to handle any requests that are already under SSL but should not be. 
            if (Request.IsSecureConnection)
            {
                Boolean requireHttps = false;
                requireHttps = filterContext.ActionDescriptor.GetCustomAttributes(typeof(RequireHttpsAttribute), false).Count() >= 1;


                //If this request is under ssl but yet the controller action
                // does not require it, then redirect to the http version.
                if (!requireHttps && !filterContext.IsChildAction)
                {
                    UriBuilder uriBuilder = new UriBuilder(Request.Url);

                    //change the scheme
                    uriBuilder.Scheme = "http";
                    uriBuilder.Port = 80;

                    filterContext.Result = this.Redirect(uriBuilder.Uri.AbsoluteUri);
                }
            }

            base.OnAuthorization(filterContext);
        }
    }
}
