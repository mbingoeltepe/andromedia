using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Controllers;
using System.Web.Mvc;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class MovieToBookConnIntTest
    {
        protected static List<Media> addedMedia = new List<Media>();
        protected static string returnView = "AddMovieForBookView";
        protected static string notFoundView = "NotFound";

        protected void AddMedia(Media media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.MediaSet.AddObject(media);
            addedMedia.Add(media);
            context.SaveChanges();
        }

        protected void DeleteMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (Media media in addedMedia)
            {
                context.MediaSet.DeleteObject(media);
            }
            context.SaveChanges();

            addedMedia.Clear();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteMedia();
        }
        
        [TestMethod]
        public void FindMoviesForBookWithMovieMatchShouldReturnAddMovieForBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie1 = TestUtil.generateMovie();
            movie1.Title = "Park";
            movie1.OriginalTitle = "Park";
            AddMedia(movie1);
            Movie movie2 = TestUtil.generateMovie();
            movie2.Title = "Jurassic";
            movie2.OriginalTitle = "Jurassic";
            AddMedia(movie2);
            Movie movie3 = TestUtil.generateMovie();
            movie3.Title = "Not Related";
            movie3.OriginalTitle = "Not Related";
            AddMedia(movie3);

            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindMoviesForBook(book.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["MoviesForBook"] as SelectList;
            Assert.IsNotNull(selectList);

            var movieTitles = from item in selectList
                             select item.Text;

            Assert.AreEqual(2, selectList.Count<SelectListItem>());
            Assert.IsTrue(movieTitles.Contains<String>("Park"));
            Assert.IsTrue(movieTitles.Contains<String>("Jurassic"));
            Assert.AreEqual(returnView, result.ViewName);
        }
        
        [TestMethod]
        public void FindMoviesForBookWithoutMovieMatchShouldReturnAddMovieForBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie1 = TestUtil.generateMovie();
            movie1.Title = "ASE";
            movie1.Title = "ASE";
            AddMedia(movie1);
            Movie movie2 = TestUtil.generateMovie();
            movie2.Title = "FCK";
            movie2.Title = "FCK";
            AddMedia(movie2);
            Movie movie3 = TestUtil.generateMovie();
            movie3.Title = "Not Related";
            movie3.Title = "Not Related";
            AddMedia(movie3);

            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindMoviesForBook(book.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["MoviesForBook"] as SelectList;
            Assert.IsNotNull(selectList);

            Assert.AreEqual(0, selectList.Count<SelectListItem>());
            Assert.AreEqual(returnView, result.ViewName);
        }

        [TestMethod]
        public void FindMoviesForBookShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindMoviesForBook(1) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(notFoundView, result.ViewName);
        }
        
        [TestMethod]
        public void FindMoviesByTitleWithMovieMatchesShouldReturnAddMovieForBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie1 = TestUtil.generateMovie();
            movie1.Title = "My Movie";
            movie1.OriginalTitle = "My Movie";
            AddMedia(movie1);
            Movie movie2 = TestUtil.generateMovie();
            movie2.Title = "Your Movie";
            movie2.OriginalTitle = "Your Movie";
            AddMedia(movie2);
            Movie movie3 = TestUtil.generateMovie();
            movie3.Title = "Not Related";
            movie3.OriginalTitle = "Not Related";
            AddMedia(movie3);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["SearchMovieBar"] = "Movie";

            ViewResult result = mediaController.FindMoviesByTitle(collection, book.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["MoviesForBook"] as SelectList;
            Assert.IsNotNull(selectList);

            var movieTitles = from item in selectList
                             select item.Text;

            Assert.AreEqual(2, selectList.Count<SelectListItem>());
            Assert.IsTrue(movieTitles.Contains<String>("My Movie"));
            Assert.IsTrue(movieTitles.Contains<String>("Your Movie"));
            Assert.AreEqual(returnView, result.ViewName);
        }
        
        [TestMethod]
        public void FindMoviesByTitleWithoutMovieMatchesShouldReturnAddMovieForBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie1 = TestUtil.generateMovie();
            movie1.Title = "ABCD";
            movie1.OriginalTitle = "ABCD";
            AddMedia(movie1);
            Movie movie2 = TestUtil.generateMovie();
            movie2.Title = "HAHAHA";
            movie2.OriginalTitle = "HAHAHA";
            AddMedia(movie2);
            Movie movie3 = TestUtil.generateMovie();
            movie3.Title = "Not Related";
            movie3.OriginalTitle = "Not Related";
            AddMedia(movie3);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["SearchMovieBar"] = "Movie";

            ViewResult result = mediaController.FindMoviesByTitle(collection, book.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["MoviesForBook"] as SelectList;
            Assert.IsNotNull(selectList);

            Assert.AreEqual(0, selectList.Count<SelectListItem>());
            Assert.AreEqual(returnView, result.ViewName);
        }
        
        [TestMethod]
        public void FindMoviesByTitleShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();
            FormCollection collection = new FormCollection();
            collection["SearchMovieBar"] = "Movie";

            ViewResult result = mediaController.FindMoviesByTitle(collection, 1) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(notFoundView, result.ViewName);
        }
        
        [TestMethod]
        public void AddMovieToBookShouldConnectAndReturnBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["MovieForBook"] = movie.Id.ToString();

            RedirectToRouteResult result = mediaController.AddMovieToBook(collection, book.Id) as RedirectToRouteResult;

            Assert.AreEqual(book.Id, movie.Book.Id);
            Assert.AreEqual(movie.Id, book.Movie.Id);
            Assert.IsNotNull(result);
            object bookId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out bookId));
            Assert.AreEqual(book.Id, (int)bookId);
        }
        
        [TestMethod]
        public void AddMovieToBookWithFalseNumberFormatShouldReturnNotFoundView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["MovieForBook"] = "xx";

            ViewResult result = mediaController.AddMovieToBook(collection, book.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
        
        [TestMethod]
        public void AddMovieToBookWithNoMovieShouldReturnNotFoundView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["MovieForBook"] = movie.Id.ToString();

            ViewResult result = mediaController.AddMovieToBook(collection, -1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void AddMovieToBookWithNoBookShouldReturnNotFoundView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["BooksForMovie"] = "-1";

            ViewResult result = mediaController.AddMovieToBook(collection, book.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
        
        [TestMethod]
        public void DeleteMovieFromBookConnShouldDeleteConnAndReturnBookView()
        {
            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            movie.Book = book;
            AddMedia(movie);

            MediaController mediaController = new MediaController();

            RedirectToRouteResult result = mediaController.DeleteMovieFromBookConn(book.Id) as RedirectToRouteResult;

            Assert.IsNull(movie.Book);
            Assert.IsNull(book.Movie);
            Assert.IsNotNull(result);
            object bookId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out bookId));
            Assert.AreEqual(book.Id, (int)bookId);
        }

        [TestMethod]
        public void DeleteMovieFromBookConnWithNoMovieShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.DeleteMovieFromBookConn(-1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}