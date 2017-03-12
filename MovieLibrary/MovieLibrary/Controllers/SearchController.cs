using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Service.ServicesImpl;

namespace MovieLibrary.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        #region Details

        [HttpGet]
        public PartialViewResult MovieDetails(int mediaId)
        {
            //TheMovieDB.TmdbAPI api = new TheMovieDB.TmdbAPI(
            //TheMovieDB.TmdbMovie movie = new TheMovieDB.TmdbMovie();
            Movie mov = MediaService.Instance.GetMovieById(mediaId);

            return PartialView("MovieDetails", mov);
        }

        [HttpGet]
        public PartialViewResult BookDetails(int mediaId)
        {
            //TheMovieDB.TmdbAPI api = new TheMovieDB.TmdbAPI(
            //TheMovieDB.TmdbMovie movie = new TheMovieDB.TmdbMovie();
            Book book = MediaService.Instance.GetBookById(mediaId);

            return PartialView("BookDetails", book);
        }

        [HttpGet]
        public PartialViewResult TvShowDetails(int mediaId)
        {
            //TheMovieDB.TmdbAPI api = new TheMovieDB.TmdbAPI(
            //TheMovieDB.TmdbMovie movie = new TheMovieDB.TmdbMovie();
            TV_Show show = MediaService.Instance.GetTvShowById(mediaId);

            return PartialView("TvShowDetails", show);
        }

        #endregion

        //
        // POST: /Home/Results
        public ActionResult Results(FormCollection collection)
        {
            if (!collection["TextBoxSearch"].Equals(string.Empty))
            {
                List<Media> mediaList = new List<Media>();
                string whereToSearch;
                if (collection["Types"] == null) // wenn die Types - Collection NULL ist, hat der user auf der master page "zitate" suche ausgewählt:
                {
                    whereToSearch = "Zitate";
                }
                else // wenn der user nach medien suchen will, muss die auswahl in der dropdown-box abgefragt werden:
                {
                    whereToSearch = collection["Types"];
                }
                string searchString = collection["TextBoxSearch"];
                string searchType = collection["radio"];



                if (whereToSearch.Equals("Alle Medien"))
                {
                    return ResultAllMedia(searchString);
                }
                else if (whereToSearch.Equals("Bücher"))
                {
                    return ResultBooks(searchString);
                }
                else if (whereToSearch.Equals("Filme"))
                {
                    return ResultMovies(searchString);
                }
                else if (whereToSearch.Equals("Zitate"))
                {
                    return ResultQuotes(searchString);
                }
                else if (whereToSearch.Equals("TV Serien"))
                {
                    return ResultTvShows(searchString);
                }
            }

            return Redirect("../");
        }

        #region ResultViews

        public ActionResult ResultQuotes(string searchString)
        {
            var list = AllQuotesService.Instance.GetQuoteByQuoteStringRanked(searchString);
            return View("ResultQuotes", list);
        }

        public ActionResult ResultBooks(string searchString)
        {
            var result = MediaService.Instance.GetBookByTitle(searchString);
            return View("ResultAllMedia", result.ToList());
        }

        public ActionResult ResultMovies(string searchString)
        {
            var result = MediaService.Instance.GetMovieByTitle(searchString);
            return View("ResultAllMedia", result.ToList());
        }

        public ActionResult ResultTvShows(string searchString)
        {
            var result = MediaService.Instance.GetTvShowByTitle(searchString);
            return View("ResultAllMedia", result.ToList());
        }

        public ActionResult ResultAllMedia(string searchString)
        {
            var result = MediaService.Instance.GetMediaByTitle(searchString);
            return View("ResultAllMedia", result.ToList());
        }

        #endregion
    }
}
