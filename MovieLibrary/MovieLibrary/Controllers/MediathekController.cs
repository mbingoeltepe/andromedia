using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Service.IServices;
using System.Security.Principal;

namespace MovieLibrary.Controllers
{
    public class MediathekController : BaseController
    {
        private IUserMediaService userMediaService;

        public MediathekController()
        {
            userMediaService = UserMediaService.Instance;
        }

        public MediathekController(IUserMediaService userMediaService)
        {
            this.userMediaService = userMediaService;
        }

        #region deleteMedia
        [HttpPost, Authorize]
        public ActionResult DeleteBook(int id, IPrincipal user)
        {
            UserBook userBook = userMediaService.GetBookById(id);

            if (userBook == null)
            {
                return View("NotFound");
            }

            Book book = userBook.Book;

            if (userBook.User.Username != user.Identity.Name)
            {
                return View("NotAuthorized");
            }

            userMediaService.DeleteBook(userBook);

            return RedirectToAction("Book", "Media", new { id = book.Id });
        }

        [HttpPost, Authorize]
        public ActionResult DeleteTvShow(int id, IPrincipal user)
        {
            UserTV_Show userTvShow = userMediaService.GetTvShowById(id);

            if (userTvShow == null)
            {
                return View("NotFound");
            }

            TV_Show tvShow = userTvShow.Season.TV_Show;

            if (userTvShow.User.Username != user.Identity.Name)
            {
                return View("NotAuthorized");
            }

            userMediaService.DeleteTvShow(userTvShow);

            return RedirectToAction("TVShow", "Media", new { id = tvShow.Id });
        }

        [HttpPost, Authorize]
        public ActionResult DeleteMovie(int id, IPrincipal user)
        {
            UserMovie userMovie = userMediaService.GetMovieById(id);

            if (userMovie == null)
            {
                return View("NotFound");
            }

            Movie movie = userMovie.Movie;

            if (userMovie.User.Username != user.Identity.Name)
            {
                return View("NotAuthorized");
            }

            userMediaService.DeleteMovie(userMovie);

            return RedirectToAction("Movie", "Media", new { id = movie.Id });

        }

        #endregion

        #region addMedia
        [HttpPost, Authorize]
        public ActionResult AddBook(int id, FormCollection collection, IPrincipal iuser)
        {
            Book book = MediaService.Instance.GetBookById(id);
            UserBook userBook = new UserBook();

            userBook.Book = book;

            User user = MembershipDao.Instance.GetCurrentUser(iuser.Identity.Name);
            userBook.User = user;

            userBook.MediaStatus = Models.UserMediaStatusEnum.NichtVerborgt.ToString();
            userBook.StoragePlace = collection["Storage"];

            UserMediaService.Instance.AddBook(userBook);

            IQueryable<UserBook> userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(iuser.Identity.Name, book.Id);

            return RedirectToAction("Book", "Media", new { id = book.Id });
        }

        [HttpPost, Authorize]
        public ActionResult AddMovie(int id, FormCollection collection, IPrincipal iuser)
        {

            Movie movie = MediaService.Instance.GetMovieById(id);
            UserMovie userMovie = new UserMovie();

            userMovie.Movie = movie;

            User user = MembershipDao.Instance.GetCurrentUser(iuser.Identity.Name);
            userMovie.User = user;

            userMovie.MediaStatus = Models.UserMediaStatusEnum.NichtVerborgt.ToString();
            userMovie.StoragePlace = collection["Storage"];
            userMovie.StorageType = collection["Device"];

            UserMediaService.Instance.AddMovie(userMovie);


            if (movie.GetType() == typeof(Movie))
            {
                return RedirectToAction("Movie", "Media", new { id = movie.Id });
            }
            else
            {
                throw new ArgumentException();
            }
        }


        [HttpPost, Authorize]
        public ActionResult AddTvShow(FormCollection collection, IPrincipal iuser)
        {
            int seasonId;

            try
            {
                seasonId = Convert.ToInt32(collection["Season"]);
            }
            catch (FormatException)
            {
                return View("NotFound");
            }

            Season season = MediaService.Instance.GetSeasonById(seasonId);

            UserTV_Show userTvShow = new UserTV_Show();

            userTvShow.Season = season;

            User user = MembershipDao.Instance.GetCurrentUser(iuser.Identity.Name);
            userTvShow.User = user;

            userTvShow.MediaStatus = Models.UserMediaStatusEnum.NichtVerborgt.ToString();
            userTvShow.StoragePlace = collection["Storage"];
            userTvShow.StorageType = collection["Device"];

            UserMediaService.Instance.AddTvShow(userTvShow);

            return RedirectToAction("TVShow", "Media", new { id = season.TV_Show.Id });
        }

        #endregion

        #region show&FilterUserMedia

        [Authorize]
        public ActionResult ShowUserBooks(string username, IPrincipal user)
        {
            if (username == null)
                return View("../User/ShowUserBooks", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(user.Identity.Name));
            else
                return View("../User/ShowUserBooks", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(username));
        }

        [Authorize]
        public PartialViewResult RenderUserBooksPartial(string username, IPrincipal user)
        {
            string finalUsername = string.Empty;
            if (username == null)
            {
                finalUsername = user.Identity.Name;
            }
            else
                finalUsername = username;

            List<UserMedia> med = new List<UserMedia>();
            med.AddRange((MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.
            GetUserBooksByUserName(finalUsername)));
            ProfileMediaViewModel model = new ProfileMediaViewModel(med, finalUsername, UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList());
            return PartialView("../User/UserBookView", model);
        }

        [Authorize]
        public PartialViewResult FilterUserBooks(string username, FormCollection collection, IPrincipal user)
        {
            UserBook book = new UserBook();
            book.Book = new Book();
            book.Book.Title = collection["TitleBox"];
            string borrowedFrom = collection["BorrowedFromCheckBox"];
            string borrowedTo = collection["BorrowedToCheckBox"];
            string author = collection["AuthorBox"];
            string finalUsername = string.Empty;
            if (username == null)
                finalUsername = user.Identity.Name;
            else
                finalUsername = username;
        
            book.User = MembershipService.Instance.GetCurrentUser(finalUsername);
            
            List<BorrowedDetails> borrowedShows = new List<BorrowedDetails>();
            foreach (var i in UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList())
            {
                if (i.UserMedia.GetType() == typeof(UserBook) && i.DateOfReturn > DateTime.Now)
                {
                    borrowedShows.Add(i);
                }
            }
            
            if (borrowedTo.Contains("true"))
            {
                book.MediaStatus = UserMediaStatusEnum.Verborgt.ToString();
            }
            book.StoragePlace = collection["StoragePlaceList"];
            if (!author.Equals(string.Empty))
            {
                Author aut = new Author();
                aut.LastName = author;
                book.Book.Author.Add(aut);
            }

            List<UserMedia> books = new List<UserMedia>();
            if (borrowedFrom.Equals("false"))
            {
                books.AddRange(UserMediaService.Instance.GetBookByUserMedia(book).ToList());
            }
            if (borrowedTo.Equals("false"))
            {
                List<UserMedia> borrowedUserBooks = new List<UserMedia>();
                List<BorrowedDetails> finalBorrowList = new List<BorrowedDetails>();
                foreach (var sh in borrowedShows)
                {
                    book.User = null;
                    borrowedUserBooks.AddRange(UserMediaService.Instance.GetBookByUserMedia(book).ToList());
                    foreach (var b in borrowedUserBooks)
                    {
                        if (sh.UserMedia.Id == b.Id)
                        {
                            finalBorrowList.Add(sh);
                        }
                    }
                }

                ProfileMediaViewModel model = new ProfileMediaViewModel(books, user.Identity.Name, finalBorrowList);
                return PartialView("../User/UserBookView", model);
            }
            ProfileMediaViewModel modelWithoutBorrowed = new ProfileMediaViewModel(books, user.Identity.Name, new List<BorrowedDetails>());
            return PartialView("../User/UserTvShowView", modelWithoutBorrowed);
        
        }

        [Authorize]
        public ActionResult ShowUserMovies(string username, IPrincipal user)
        {
            if(username == null)
                return View("../User/ShowUserMovies", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(user.Identity.Name));
            else
                return View("../User/ShowUserMovies", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(username));
        }

        [Authorize]
        public PartialViewResult RenderUserMoviesPartial(string username, IPrincipal user)
        {
            string finalUsername = string.Empty;
            if (username == null)
            {
                finalUsername = user.Identity.Name;
            }
            else
                finalUsername = username;

            List<UserMedia> med = new List<UserMedia>();
            med.AddRange((MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.
            GetUserMoviesByUserName(finalUsername)));
            ProfileMediaViewModel model = new ProfileMediaViewModel(med, finalUsername, UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList());
            return PartialView("../User/UserMovieView", model);
        }

        /// <summary>
        /// Medthode um Movies zu filtern
        /// </summary>
        /// <param name="collection">Elemente die aus Form übergeben wurden</param>
        /// <param name="user">Angemeldeter User</param>
        /// <returns></returns>
        [Authorize]
        public PartialViewResult FilterUserMovie(string username, FormCollection collection, IPrincipal user)
        {
            UserMovie movie = new UserMovie();
            movie.Movie = new Movie();
            movie.Movie.Title = collection["TitleBox"];
            string borrowedFrom = collection["BorrowedFromCheckBox"];
            string borrowedTo = collection["BorrowedToCheckBox"];
            string finalUsername = string.Empty;
            if (username == null)
                finalUsername = user.Identity.Name;
            else
                finalUsername = username;
            movie.User = MembershipService.Instance.GetCurrentUser(finalUsername);
            
            List<BorrowedDetails> borrowedShows = new List<BorrowedDetails>();
            foreach (var i in UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList())
            {
                if (i.UserMedia.GetType() == typeof(UserMovie) && i.DateOfReturn > DateTime.Now)
                {
                    borrowedShows.Add(i);
                }
            }

            if (borrowedTo.Contains("true"))
            {
                movie.MediaStatus = UserMediaStatusEnum.Verborgt.ToString();
            }
            movie.StoragePlace = collection["StoragePlaceList"];
            movie.StorageType = collection["TypeList"];

            List<UserMedia> movies = new List<UserMedia>();
            if (borrowedFrom.Equals("false"))
            {
                movies.AddRange(UserMediaService.Instance.GetMovieByUserMedia(movie).ToList());
            }
            if (borrowedTo.Equals("false"))
            {
                List<UserMedia> borrowedUserShows = new List<UserMedia>();
                List<BorrowedDetails> finalBorrowList = new List<BorrowedDetails>();
                foreach (var sh in borrowedShows)
                {
                    movie.User = null;
                    borrowedUserShows.AddRange(UserMediaService.Instance.GetMovieByUserMedia(movie).ToList());
                    foreach (var b in borrowedUserShows)
                    {
                        if (sh.UserMedia.Id == b.Id)
                        {
                            finalBorrowList.Add(sh);
                        }
                    }
                }

                ProfileMediaViewModel model = new ProfileMediaViewModel(movies, user.Identity.Name, finalBorrowList);
                return PartialView("../User/UserMovieView", model);
            }
            ProfileMediaViewModel modelWithoutBorrowed = new ProfileMediaViewModel(movies, user.Identity.Name, new List<BorrowedDetails>());
            return PartialView("../User/UserTvShowView", modelWithoutBorrowed);
        }

        [Authorize]
        public ActionResult ShowUserTVShows(string username, IPrincipal user)
        {
            if (username == null)
                return View("../User/ShowUserTVShows", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(user.Identity.Name));
            else
                return View("../User/ShowUserTVShows", MovieLibrary.Service.ServicesImpl.MembershipService.Instance.GetCurrentUser(username));
        }

        [Authorize]
        public PartialViewResult RenderTVShowsPartial(string username, IPrincipal user)
        {
            string finalUsername = string.Empty;
            if (username == null)
            {
                finalUsername = user.Identity.Name;
            }
            else
                finalUsername = username;
            
            List<UserMedia> med = new List<UserMedia>();
            med.AddRange((MovieLibrary.Service.ServicesImpl.UserMediaService.Instance.
            GetUserTvShowsByUserName(finalUsername)));
            ProfileMediaViewModel model = new ProfileMediaViewModel(med, finalUsername, UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList());
            return PartialView("../User/UserTvShowView", model);
        }

        /// <summary>
        /// Methode um TV-Shows zu filtern
        /// </summary>
        /// <param name="collection">Alle Elemente aus der Form</param>
        /// <param name="user">Angemeldeter User</param>
        /// <returns></returns>
        [Authorize]
        public PartialViewResult FilterUserTvShow(string username, FormCollection collection, IPrincipal user)
        {
            UserTV_Show show = new UserTV_Show();
            show.Season = new Season();
            show.Season.TV_Show = new TV_Show();
            show.Season.TV_Show.Title = collection["TitleBox"];
            string seasonNr = collection["SeasonList"];
            string borrowedFrom = collection["BorrowedFromCheckBox"];
            string borrowedTo = collection["BorrowedToCheckBox"];
            string finalUsername = string.Empty;
            if (!seasonNr.Equals(string.Empty))
            {
                show.Season.Number = Int32.Parse(seasonNr);
            }
            if (username == null)
                finalUsername = user.Identity.Name;
            else
                finalUsername = username;

            show.User = MembershipService.Instance.GetCurrentUser(finalUsername);

            List<BorrowedDetails> borrowedShows = new List<BorrowedDetails>();
            foreach (var i in UserMediaService.Instance.GetAllBorrowedFromMediaByUser(finalUsername).ToList())
            {
                if (i.UserMedia.GetType() == typeof(UserTV_Show) && i.DateOfReturn > DateTime.Now)
                {
                    borrowedShows.Add(i);
                }
            }
            
            if (borrowedTo.Contains("true"))
            {
                show.MediaStatus = UserMediaStatusEnum.Verborgt.ToString();
            }
            show.StoragePlace = collection["StoragePlaceList"];
            show.StorageType = collection["TypeList"];
            List<UserMedia> shows = new List<UserMedia>();
            if (borrowedFrom.Equals("false"))
            {
                shows.AddRange(UserMediaService.Instance.GetTvShowByUserMedia(show).ToList());
            }
            if (borrowedTo.Equals("false"))
            {
                List<UserMedia> borrowedUserShows = new List<UserMedia>();
                List<BorrowedDetails> finalBorrowList = new List<BorrowedDetails>();
                foreach (var sh in borrowedShows)
                {
                    show.User = null;
                    borrowedUserShows.AddRange(UserMediaService.Instance.GetTvShowByUserMedia(show).ToList());
                    foreach (var b in borrowedUserShows)
                    {
                        if (sh.UserMedia.Id == b.Id)
                        {
                            finalBorrowList.Add(sh);
                        }
                    }
                }
                ProfileMediaViewModel model = new ProfileMediaViewModel(shows, user.Identity.Name, finalBorrowList);
                return PartialView("../User/UserTvShowView", model);
            }
            ProfileMediaViewModel modelWithoutBorrowed = new ProfileMediaViewModel(shows, user.Identity.Name, new List<BorrowedDetails>());
            return PartialView("../User/UserTvShowView", modelWithoutBorrowed);
        }

        #endregion

        #region borrowMedia

        public RedirectToRouteResult SendBorrowRequest(int userMediaId, IPrincipal user)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);
            UserMediaService.Instance.BorrowRequestUserMedia(user.Identity.Name, userMedia);

            if (userMedia.GetType() == typeof(UserMovie))
                return RedirectToAction("Movie", "Media", new { username = userMedia.User.Username, id = ((UserMovie)userMedia).MovieId });
            else if (userMedia.GetType() == typeof(UserBook))
                return RedirectToAction("Book", "Media", new { username = userMedia.User.Username, id = ((UserBook)userMedia).BookId});
            else if (userMedia.GetType() == typeof(UserTV_Show))
                return RedirectToAction("TVShow", "Media", new { username = userMedia.User.Username, id = ((UserTV_Show)userMedia).Season.TV_ShowId });
            else return null;
        }

        public PartialViewResult BorrowDetails(int userMediaId)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);

            IQueryable<BorrowedDetails> borrowed = UserMediaService.Instance.GetAllBorrowedAwayMediaByUser(userMedia.User.Username);
            try
            {
                var detail = (from i in borrowed
                              where i.UserMedia.Id == userMediaId &&
                              i.DateOfReturn.CompareTo(DateTime.Now) > 0
                              select i).Single();

                BorrowedMediaDetail det = new BorrowedMediaDetail(detail, detail.UserMedia);
                return PartialView("../User/ShowBorrowDetails", det);
                
            }
            catch (Exception)
            {
                return PartialView("../User/ShowBorrowDetails", null);
            }
        }

        public RedirectToRouteResult TakeBorrowedMediaBack(int userMediaId, IPrincipal user)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);
            
            userMedia.MediaStatus = Models.UserMediaStatusEnum.NichtVerborgt.ToString();
            
            IQueryable<BorrowedDetails> borrowed = UserMediaService.Instance.GetAllBorrowedAwayMediaByUser(userMedia.User.Username);

            var details = (from i in borrowed
                           where i.UserMedia.Id == userMediaId &&
                           i.DateOfReturn > DateTime.Now
                           select i).Single();
            
            details.DateOfReturn = DateTime.Now;
            details.TakeBackRequest = false;
            UserMediaService.Instance.Save();

            if (userMedia.GetType() == typeof(UserMovie))
                return RedirectToAction("Movie", "Media", new { username = userMedia.User.Username, id = ((UserMovie)userMedia).MovieId });
            else if (userMedia.GetType() == typeof(UserBook))
                return RedirectToAction("Book", "Media", new { username = userMedia.User.Username, id = ((UserBook)userMedia).BookId });
            else if (userMedia.GetType() == typeof(UserTV_Show))
                return RedirectToAction("TVShow", "Media", new { username = userMedia.User.Username, id = ((UserTV_Show)userMedia).Season.TV_ShowId });
            else return null;
        }

        public RedirectToRouteResult GiveBorrowedMediaBackRequest(int userMediaId, IPrincipal user)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);

            IQueryable<BorrowedDetails> borrowed = UserMediaService.Instance.GetAllBorrowedFromMediaByUser(user.Identity.Name);

            var borrowedMedia = (from i in borrowed
                                 where i.UserMedia.Id == userMedia.Id &&
                                 i.DateOfReturn > DateTime.Now
                                 select i).Single();
            borrowedMedia.TakeBackRequest = true;
            UserMediaService.Instance.Save();

            if (userMedia.GetType() == typeof(UserMovie))
                return RedirectToAction("Movie", "Media", new { username = userMedia.User.Username, id = ((UserMovie)userMedia).MovieId });
            else if (userMedia.GetType() == typeof(UserBook))
                return RedirectToAction("Book", "Media", new { username = userMedia.User.Username, id = ((UserBook)userMedia).BookId });
            else if (userMedia.GetType() == typeof(UserTV_Show))
                return RedirectToAction("TVShow", "Media", new { username = userMedia.User.Username, id = ((UserTV_Show)userMedia).Season.TV_ShowId });
            else return null;
        }

        public RedirectToRouteResult BorrowMediaToUser(string name, int userMediaId, IPrincipal user, FormCollection collection)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);

            if (name == null)
            {
                string formUsername = collection["TextBoxNameTo"];
                if (formUsername.Equals(string.Empty))
                {
                    formUsername = collection["User.Friends"];
                }
                UserMediaService.Instance.BorrowUserMediaToUser(formUsername, userMedia);
            }
            else 
            {
                UserMediaService.Instance.BorrowUserMediaToUser(name, userMedia);
            }
            
            if (userMedia.GetType() == typeof(UserMovie))
                return RedirectToAction("Movie", "Media", new { username = userMedia.User.Username, id = ((UserMovie)userMedia).MovieId });
            else if (userMedia.GetType() == typeof(UserBook))
                return RedirectToAction("Book", "Media", new { username = userMedia.User.Username, id = ((UserBook)userMedia).BookId });
            else if (userMedia.GetType() == typeof(UserTV_Show))
                return RedirectToAction("TVShow", "Media", new { username = userMedia.User.Username, id = ((UserTV_Show)userMedia).Season.TV_ShowId });
            else return null;
        }

        public RedirectToRouteResult DiscardBorrowMediaToUser(string name, int userMediaId, IPrincipal user, FormCollection collection)
        {
            UserMedia userMedia = UserMediaService.Instance.GetUserMediaById(userMediaId);
            
            UserMediaService.Instance.DiscardBorrowUserMediaToUser(name, userMedia);

            if (userMedia.GetType() == typeof(UserMovie))
                return RedirectToAction("Movie", "Media", new { username = userMedia.User.Username, id = ((UserMovie)userMedia).MovieId });
            else if (userMedia.GetType() == typeof(UserBook))
                return RedirectToAction("Book", "Media", new { username = userMedia.User.Username, id = ((UserBook)userMedia).BookId });
            else if (userMedia.GetType() == typeof(UserTV_Show))
                return RedirectToAction("TVShow", "Media", new { username = userMedia.User.Username, id = ((UserTV_Show)userMedia).Season.TV_ShowId });
            else return null;
        }

        public ActionResult ShowBorrowStatistics(IPrincipal user)
        {

            return View("../User/ShowBorrowStatistics");
            //IQueryable<BorrowedDetails> borrowedAway = userMediaService.GetAllBorrowedAwayMediaByUser(user.Identity.Name);
            //IQueryable<BorrowedDetails> borrowedFrom = userMediaService.GetAllBorrowedFromMediaByUser(user.Identity.Name);
        }

        #endregion
    }
}
