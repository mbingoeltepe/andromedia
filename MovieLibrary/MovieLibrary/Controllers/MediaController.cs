using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Models;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service;
using System.Security.Principal;

namespace MovieLibrary.Controllers
{
    public class MediaController : BaseController
    {
        IImageService imageService;

        public MediaController()
        {
            imageService = new AWSImageService();
        }

        [HttpGet]
        public ActionResult TVShow(string username, int id)
        {
            TV_Show tvshow = MediaService.Instance.GetTvShowById(id);

            if (tvshow == null)
            {
                return View("NotFound");
            }

            IQueryable<UserTV_Show> userTVShows;
            TVShowDetailsViewModel tvshowDetails;
            Image image = null;
            try
            {
                    image = imageService.GetImagesForVideo(tvshow)[AWSImageService.MEDIUMIMAGE];
            }
            catch(Exception)
            {}
            if (image == null)
                image = AWSImageService.IMAGE_NOIMAGEFOUND;

            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name.Equals(username) || username == null)
                {
                    userTVShows = UserMediaService.Instance.GetUserTvShowByIdAndUserName(User.Identity.Name, id);
                    tvshowDetails = new TVShowDetailsViewModel(tvshow, image, userTVShows, MembershipService.Instance.GetCurrentUser(User.Identity.Name));
                }
                else if (MembershipService.Instance.GetCurrentUser(User.Identity.Name).Friends.Contains(MembershipService.Instance.GetCurrentUser(username)))
                {
                    userTVShows = UserMediaService.Instance.GetUserTvShowByIdAndUserName(username, id);
                    tvshowDetails = new TVShowDetailsViewModel(tvshow, image, userTVShows, MembershipService.Instance.GetCurrentUser(username));
                }
                else if (!MembershipService.Instance.GetCurrentUser(User.Identity.Name).Friends.Contains(MembershipService.Instance.GetCurrentUser(username)))
                {
                    userTVShows = UserMediaService.Instance.GetUserTvShowByIdAndUserName(User.Identity.Name, id);
                    tvshowDetails = new TVShowDetailsViewModel(tvshow, image, userTVShows, MembershipService.Instance.GetCurrentUser(User.Identity.Name));
                }
                else
                {
                    tvshowDetails = new TVShowDetailsViewModel(tvshow, image);
                }
            }
            else
            {
                tvshowDetails = new TVShowDetailsViewModel(tvshow, image);
            }
            return View("TVShowDetails", tvshowDetails);
        }

        [HttpGet]
        public ActionResult Movie(string username, int id)
        {
            Movie movie = MediaService.Instance.GetMovieById(id);

            if (movie == null)
            {
                return View("NotFound");
            }

            IQueryable<UserMovie> userMovies;
            MovieDetailsViewModel movieDetails;
            Image image = null;
            try
            {
                image = imageService.GetImagesForVideo(movie)[AWSImageService.MEDIUMIMAGE];
            }
            catch (Exception)
            { }
            if (image == null)
                image = AWSImageService.IMAGE_NOIMAGEFOUND;

            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name.Equals(username) || username == null)
                {
                    userMovies = UserMediaService.Instance.GetUserMovieByIdAndUserName(User.Identity.Name, id);
                    movieDetails = new MovieDetailsViewModel(movie, image, userMovies, MembershipService.Instance.GetCurrentUser(User.Identity.Name));
                }
                else if (MembershipService.Instance.GetCurrentUser(User.Identity.Name).Friends.Contains(MembershipService.Instance.GetCurrentUser(username)))
                {
                    userMovies = UserMediaService.Instance.GetUserMovieByIdAndUserName(username, id);
                    movieDetails = new MovieDetailsViewModel(movie, image, userMovies, MembershipService.Instance.GetCurrentUser(username));
                }
                else
                {
                    movieDetails = new MovieDetailsViewModel(movie, image);
                }
            }
            else
            {
                movieDetails = new MovieDetailsViewModel(movie, image);
            }
            return View("MovieDetails", movieDetails);
        }

        [HttpGet]
        public ActionResult Book(string username, int id)
        {
            Book book = MediaService.Instance.GetBookById(id);

            if (book == null)
            {
                return View("NotFound");
            }

            IQueryable<UserBook> userBooks;
            BookDetailsViewModel bookDetails;
            Image image = null;
            try
            {
                image = imageService.GetImagesForBook(book)[AWSImageService.MEDIUMIMAGE];
            }
            catch (Exception) { }
            if (image == null)
                image = AWSImageService.IMAGE_NOIMAGEFOUND;

            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name.Equals(username) || username == null)
                {
                    userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(User.Identity.Name, id);
                    bookDetails = new BookDetailsViewModel(book, image, userBooks, MembershipService.Instance.GetCurrentUser(User.Identity.Name));
                }
                else if (MembershipService.Instance.GetCurrentUser(User.Identity.Name).Friends.Contains(MembershipService.Instance.GetCurrentUser(username)))
                {
                    userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(username, id);
                    bookDetails = new BookDetailsViewModel(book, image, userBooks, MembershipService.Instance.GetCurrentUser(username));
                }
                else
                {
                    bookDetails = new BookDetailsViewModel(book, image);
                }   
            }
            else
            {
                bookDetails = new BookDetailsViewModel(book, image);
            }

            return View("BookDetails", bookDetails);
        }

        #region Actor zu Video hinzu

        public ActionResult AddActorToVideo(int id)
        {
            Media movie = MediaService.Instance.GetMediaById(id);
            return View(movie);
        }

        public PartialViewResult RefreshActorList(string name)
        {
            List<Actor> actors = PersonService.Instance.GetActor(name).ToList();

            return PartialView("PersonList", actors);
        }

        [HttpPost]
        public ActionResult AddActorMovie(FormCollection collection, int id)
        {
            Movie video = MediaService.Instance.GetMovieById(id);
            string name = collection["Persons"];
            Actor actor = PersonService.Instance.GetActor(name).Single();
            if (!video.Actor.Contains(actor))
            {
                video.Actor.Add(actor);
                MediaService.Instance.SaveChangesMovie();
            }
            return RedirectToAction("Movie", new { id = id });
        }

        public ActionResult AddActorTvShow(FormCollection collection, int id)
        {
            TV_Show video = MediaService.Instance.GetTvShowById(id);
            string name = collection["Persons"];
            Actor actor = PersonService.Instance.GetActor(name).Single();
            if (!video.Actor.Contains(actor))
            {
                video.Actor.Add(actor);
                MediaService.Instance.SaveChangesTvShow();
            }
            return RedirectToAction("TVShow", new { id = id });
        }

        #endregion

        #region Autor zu Buch hinzu

        public ActionResult AddAuthorToBook(int id)
        {
            Book book = MediaService.Instance.GetBookById(id);
            return View(book);
        }

        public PartialViewResult RefreshAuthorList(string name)
        {
            List<Author> authors = PersonService.Instance.GetAuthor(name).ToList();

            return PartialView("PersonList", authors);
        }
        [HttpPost]
        public ActionResult AddAuthor(FormCollection collection, int id)
        {
            Book book = MediaService.Instance.GetBookById(id);
            string name = collection["Persons"];
            Author author = PersonService.Instance.GetAuthor(name).Single();
            if (!book.Author.Contains(author))
            {
                book.Author.Add(author);
                MediaService.Instance.SaveChangesBook();
            }
            return RedirectToAction("Book", new { id = id });
        }

        #endregion

        #region Andromedia erweitern

        /// <summary>
        /// Entscheidet, welcher View zum Erweitern angezeigt werden soll
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ShowExtendView(FormCollection collection)
        {
            string chosenOne = collection["ChooseMedia"];

            if (chosenOne != null)
            {
                if (chosenOne.Equals("movie"))
                {
                    return View("AddMovieToAndromedia");
                }
                else if (chosenOne.Equals("book"))
                {
                    return View("AddBookToAndromedia");
                }
                else if (chosenOne.Equals("tvShow"))
                {
                    return View("AddTvShowToAndromedia");
                }
                else if (chosenOne.Equals("actor"))
                {
                    return View("AddActorToAndromedia");
                }
                else if (chosenOne.Equals("author"))
                {
                    return View("AddAuthorToAndromedia");
                }
            }
            return Redirect("/");
        }

        /// <summary>
        /// Fügt ein neues Buch zur DB hinzu und erstellt einen Request.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddMovieToDb(FormCollection collection, IPrincipal user)
        {
            string title = collection["ctl00$MainContent$MovieTitle"];
            string originalTitle = collection["ctl00$MainContent$MovieTitleOriginal"];
            string genre = collection["GenreList"];
            string date = collection["ctl00$MainContent$Datepicker"];
            
            DateTime releaseDate = DateTime.MaxValue;
            try
            {
                if (date.Length == 4)
                {
                    DateTime year = new DateTime(Int32.Parse(date), 01, 01);
                    releaseDate = year;
                }
                else
                    releaseDate = DateTime.Parse(date);
            }
            catch (Exception)
            { 
                
            }

            Models.Movie movie = new Movie();
            movie.Title = title;
            movie.OriginalTitle = originalTitle;
            movie.Genre = genre;
            movie.ReleaseDate = releaseDate;
            movie.Pending = true;
            movie.AddingDate = DateTime.Now;

            InsertRequest request = new InsertRequest();
            request.RequestDate = DateTime.Now;
            request.Media = movie;
            request.User = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            
            MediaService.Instance.AddMovie(movie);
            return View("ConfirmationAddedMedia");
        }
        /// <summary>
        /// Fügt ein neues Buch zur DB hinzu und erstellt einen Request.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddBookToDb(FormCollection collection, IPrincipal user)
        {
            string title = collection["ctl00$MainContent$BookTitle"];
            string originalTitle = collection["ctl00$MainContent$BookTitleOriginal"];
            string genre = collection["GenreList"];
            string isbn = collection["ctl00$MainContent$Isbn"];

            Book book = new Book();
            book.Genre = genre;
            book.Isbn = isbn;
            book.OriginalTitle = originalTitle;
            book.Pending = true;
            book.Title = title;
            book.AddingDate = DateTime.Now;

            InsertRequest request = new InsertRequest();
            request.RequestDate = DateTime.Now;
            request.Media = book;
            request.User = MembershipService.Instance.GetCurrentUser(user.Identity.Name);

            MediaService.Instance.AddBook(book);
            return View("ConfirmationAddedMedia");
        }

        /// <summary>
        /// Fügt eine neue Serie zur DB hinzu und erstellt einen Request.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddTvShowToDb(FormCollection collection, IPrincipal user)
        {
            string title = collection["ctl00$MainContent$TvShowTitle"];
            string originalTitle = collection["ctl00$MainContent$TvShowTitleOriginal"];
            string genre = collection["GenreList"];
            string dateBeginning = collection["ctl00$MainContent$DateTextBoxBeginning"];
            string dateEnding = collection["ctl00$MainContent$DateTextBoxEnding"];
            int seasons = Int32.Parse(collection["SeasonList"]);
            DateTime showBeginning = DateTime.MaxValue;
            DateTime? showEnding = null;
            try
            {
                if (dateBeginning.Length == 4)
                {
                    DateTime year = new DateTime(Int32.Parse(dateBeginning), 01, 01);
                    showBeginning = year;
                }
                else
                    showEnding = DateTime.Parse(dateEnding);
            }
            catch (Exception)
            { }
            

            TV_Show show = new TV_Show();
            show.Genre = genre;
            show.OriginalTitle = originalTitle;
            show.Pending = true;

            for (int i = 0; i < seasons; i++)
            {
                Season tvSeason = new Season();
                tvSeason.Number = i+1;
                show.Season.Add(tvSeason);
                Episode dummyEpisode = new Episode();
                dummyEpisode.Name = "E01";
                dummyEpisode.Number = 1;
                
                tvSeason.Episode.Add(dummyEpisode);
            }
            
            show.ShowBeginning = showBeginning;
            show.ShowEnding = showEnding;
            show.Title = title;
            show.AddingDate = DateTime.Now;

            InsertRequest request = new InsertRequest();
            request.RequestDate = DateTime.Now;
            request.Media = show;
            request.User = MembershipService.Instance.GetCurrentUser(user.Identity.Name);

            MediaService.Instance.AddTvShow(show);
            return View("ConfirmationAddedMedia");
        
        }
        /// <summary>
        /// Fügt einen neuen Autor zur DB hinzu
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddAuthorToDb(FormCollection collection, IPrincipal user)
        {

            Author author = new Author();
            // UpdateModel(author);

            author.FirstName = collection["ctl00$MainContent$AuthorFirstName"];
            author.LastName = collection["ctl00$MainContent$AuthorLastName"];

            if (PersonService.Instance.GetAuthorByExactName(author.FirstName, author.LastName) == null)
            {
                PersonService.Instance.AddAuthor(author);
            }
            else
            {
                return View("AuthorAlreadyThere");
            }
            return View("ConfirmationAddedMedia");
        }

        /// <summary>
        /// Fügt einen neuen Schauspieler zur DB hinzu
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddActorToDb(FormCollection collection, IPrincipal user)
        {
            Actor actor = new Actor();
            // UpdateModel(actor);

            actor.FirstName = collection["ctl00$MainContent$ActorFirstName"];
            actor.LastName = collection["ctl00$MainContent$ActorLastName"];

            if (PersonService.Instance.GetActorByExactName(actor.FirstName, actor.LastName) == null)
            {
                PersonService.Instance.AddActor(actor);
            }
            else
            {
                return View("ActorAlreadyThere");
            }
            return View("ConfirmationAddedMedia");
        }

        #endregion

        #region BookToMovieConnection
        [Authorize]
        public ActionResult FindBooksForMovie(int id)
        {
            IEnumerable<Book> books = MediaService.Instance.GetBooksForMovie(id);

            if (books == null)
            {
                return View("NotFound");
            }

            ViewData["BooksForMovie"] = new SelectList(books, "Id", "Title");

            return View("AddBookForMovieView", MediaService.Instance.GetMovieById(id));
        }

        [Authorize]
        public ActionResult FindBooksByTitle(FormCollection collection, int id)
        {
            Movie mov = MediaService.Instance.GetMovieById(id);

            if (mov == null)
            {
                return View("NotFound");
            }

            string title = collection["SearchBookBar"];
            IEnumerable<Book> books = MediaService.Instance.GetBookByTitle(title);
     
            ViewData["BooksForMovie"] = new SelectList(books, "Id", "Title");
            return View("AddBookForMovieView", mov);
        }

        [Authorize]
        public ActionResult AddBookToMovie(FormCollection collection, int id)
        {
            int bookId;

            try
            {
                bookId = Convert.ToInt32(collection["BooksForMovie"]);
            }
            catch (FormatException)
            {
                return View("NotFound");
            }

            Book book = MediaService.Instance.GetBookById(bookId);
            Movie mov = MediaService.Instance.GetMovieById(id);

            if (book == null || mov == null)
            {
                return View("NotFound");
            }

            mov.Book = book;
            MediaService.Instance.SaveChangesMovie();
            return RedirectToAction("Movie", new { id = id });
        }

        [Authorize]
        public ActionResult DeleteBookFromMovieConn(int id)
        {
            Movie mov = MediaService.Instance.GetMovieById(id);

            if (mov == null)
            {
                return View("NotFound");
            }

            mov.Book = null;
            MediaService.Instance.SaveChangesMovie();

            return RedirectToAction("Movie", new { id = id });
        }
        #endregion

        #region MovieToBookConneciton

        [Authorize]
        public ActionResult FindMoviesByTitle(FormCollection collection, int id)
        {
            Book book = MediaService.Instance.GetBookById(id);

            if (book == null)
            {
                return View("NotFound");
            }

            string title = collection["SearchMovieBar"];
            IEnumerable<Movie> movies = MediaService.Instance.GetMovieByTitle(title);

            ViewData["MoviesForBook"] = new SelectList(movies, "Id", "Title");
            return View("AddMovieForBookView", book);
        }

        [Authorize]
        public ActionResult AddMovieToBook(FormCollection collection, int id)
        {
            int movieId;

            try
            {
                movieId = Convert.ToInt32(collection["MovieForBook"]);
            }
            catch(FormatException)
            {
                return View("NotFound");
            }

            Movie movie = MediaService.Instance.GetMovieById(movieId);
            Book book = MediaService.Instance.GetBookById(id);

            if (movie == null || book == null)
            {
                return View("NotFound");
            }

            book.Movie = movie;
            MediaService.Instance.SaveChangesBook();
            return RedirectToAction("Book", new { id = id });
        }

        [Authorize]
        public ActionResult FindMoviesForBook(int id)
        {
            IEnumerable<Movie> movies = MediaService.Instance.GetMoviesForBook(id);

            if (movies == null)
            {
                return View("NotFound");
            }

            ViewData["MoviesForBook"] = new SelectList(movies, "Id", "Title");

            return View("AddMovieForBookView", MediaService.Instance.GetBookById(id));
        }

        [Authorize]
        public ActionResult DeleteMovieFromBookConn(int id)
        {
            Book book = MediaService.Instance.GetBookById(id);

            if (book == null)
            {
                return View("NotFound");
            }

            book.Movie = null;
            MediaService.Instance.SaveChangesBook();

            return RedirectToAction("Book", new { id = id });
        }
        #endregion

        public ActionResult TopRated()
        {
            return View("../Media/TopRatedMedia", MediaService.Instance.GetTopRatedMedia());
        }

        [HttpPost]
        public ActionResult Rating(int id, double rating)
        {


            ViewData["message"] = "Ihre Bewertung wurde erfolgreich gespeichert.";

            int idn = Convert.ToInt32(id);

            Book book = MediaService.Instance.GetBookById(idn);
            if (book != null)
            {
                if (CanUserVote(idn, Convert.ToInt32(rating)) == true)
                {
                    book.TotalRaters = book.TotalRaters + 1;
                    book.Rating += Convert.ToInt32(rating);
                    book.AverageRating = book.Rating / book.TotalRaters;

                    MediaService.Instance.SaveChangesBook();

                    ViewData["message"] = "Ihre Bewertung dieses Buches wurde erfolgreich gespeichert.";
                }
                else
                {
                    book.Rating -= userRating(idn);
                    book.Rating += Convert.ToInt32(rating);
                    book.AverageRating = book.Rating / book.TotalRaters;

                    MediaService.Instance.SaveChangesBook();

                    ViewData["message"] = "Ihre Bewertung dieses Buches wurde erfolgreich aktualisiert.";
                }
            }

            Movie movie = MediaService.Instance.GetMovieById(idn);
            if (movie != null)
            {
                if (CanUserVote(idn, Convert.ToInt32(rating)) == true)
                {
                    movie.TotalRaters = movie.TotalRaters + 1;
                    movie.Rating += Convert.ToInt32(rating);
                    movie.AverageRating = movie.Rating / movie.TotalRaters;

                    MediaService.Instance.SaveChangesMovie();

                    ViewData["message"] = "Ihre Bewertung dieses Films wurde erfolgreich gespeichert.";
                }
                else
                {
                    movie.Rating -= userRating(idn);
                    movie.Rating += Convert.ToInt32(rating);
                    movie.AverageRating = movie.Rating / movie.TotalRaters;

                    MediaService.Instance.SaveChangesMovie();

                    ViewData["message"] = "Ihre Bewertung dieses Films wurde erfolgreich aktualisiert.";
                }
            }

            TV_Show tv_show = MediaService.Instance.GetTvShowById(idn);
            if (tv_show != null)
            {
                if (CanUserVote(idn, Convert.ToInt32(rating)) == true)
                {
                    tv_show.TotalRaters = tv_show.TotalRaters + 1;
                    tv_show.Rating += Convert.ToInt32(rating);
                    tv_show.AverageRating = tv_show.Rating / tv_show.TotalRaters;

                    MediaService.Instance.SaveChangesTvShow();

                    ViewData["message"] = "Ihre Bewertung dieser TV-Serie wurde erfolgreich gespeichert.";
                }
                else
                {
                    tv_show.Rating -= userRating(idn);
                    tv_show.Rating += Convert.ToInt32(rating);
                    tv_show.AverageRating = tv_show.Rating / tv_show.TotalRaters;

                    MediaService.Instance.SaveChangesTvShow();

                    ViewData["message"] = "Ihre Bewertung dieser TV-Serie wurde erfolgreich aktualisiert.";
                }
            }




            return View();
        }

        private Boolean CanUserVote(int id, double rating)
        {
            HttpCookie voteCookie = Request.Cookies["Votes"];

            if (voteCookie != null)
            {
                if (voteCookie[id.ToString()] != null)
                {
                    voteCookie[id.ToString()] = rating.ToString();
                    Response.Cookies.Set(voteCookie);
                    return false;
                }
            }

            // Erstellen die Cookies und den Wert            
            voteCookie = new HttpCookie("Votes");
            voteCookie[id.ToString()] = rating.ToString();
            Response.Cookies.Add(voteCookie);
            return true;
        }

        private Double userRating(int id)
        {
            HttpCookie voteCookie = Request.Cookies["Votes"];

            if (voteCookie != null)
            {
                if (voteCookie[id.ToString()] != null)
                {
                    return Convert.ToInt32(voteCookie[id.ToString()]);
                }
            }

            return 0;
        }

    }
}
