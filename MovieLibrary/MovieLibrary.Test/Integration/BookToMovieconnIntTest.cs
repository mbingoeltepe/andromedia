using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using System.Security.Principal;
using MovieLibrary.Controllers;
using System.Web.Mvc;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class BookToMovieConnIntTest
    {
        protected static List<Media> addedMedia = new List<Media>();
        protected static string returnView = "AddBookForMovieView";
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
        public void FindBooksForMovieWithBookMatchShouldReturnAddBookForMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book1 = TestUtil.generateBook();
            book1.Title = "Park";
            book1.OriginalTitle = "Park";
            AddMedia(book1);
            Book book2 = TestUtil.generateBook();
            book2.Title = "Jurassic";
            book2.OriginalTitle = "Jurassic";
            AddMedia(book2);
            Book book3 = TestUtil.generateBook();
            book3.Title = "Not Related";
            book3.OriginalTitle = "Not Related";
            AddMedia(book3);

            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindBooksForMovie(movie.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["BooksForMovie"] as SelectList;
            Assert.IsNotNull(selectList);

            var bookTitles = from item in selectList
                             select item.Text;

            Assert.AreEqual(2, selectList.Count<SelectListItem>());
            Assert.IsTrue(bookTitles.Contains<String>("Park"));
            Assert.IsTrue(bookTitles.Contains<String>("Jurassic"));
        }

        [TestMethod]
        public void FindBooksForMovieWithoutBookMatchShouldReturnAddBookForMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book1 = TestUtil.generateBook();
            book1.Title = "Fuck";
            book1.OriginalTitle = "Fuck";
            AddMedia(book1);
            Book book2 = TestUtil.generateBook();
            book2.Title = "ASE";
            book2.OriginalTitle = "ASE";
            AddMedia(book2);
            Book book3 = TestUtil.generateBook();
            book3.Title = "Not Related";
            book3.OriginalTitle = "Not Related";
            AddMedia(book3);

            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindBooksForMovie(movie.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["BooksForMovie"] as SelectList;
            Assert.IsNotNull(selectList);

            Assert.AreEqual(0, selectList.Count<SelectListItem>());
        }

        [TestMethod]
        public void FindBooksForMovieShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.FindBooksForMovie(-1) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("NotFound" , result.ViewName);
        }

        [TestMethod]
        public void FindBooksByTitleWithBookMatchesShouldReturnAddBookForMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book1 = TestUtil.generateBook();
            book1.Title = "Nothing";
            book1.OriginalTitle = "Nothing";
            AddMedia(book1);
            Book book2 = TestUtil.generateBook();
            book2.Title = "Your Book";
            book2.OriginalTitle = "Your Book";
            AddMedia(book2);
            Book book3 = TestUtil.generateBook();
            book3.Title = "My Book";
            book3.OriginalTitle = "My Book";
            AddMedia(book3);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["SearchBookBar"] = "Book";

            ViewResult result = mediaController.FindBooksByTitle(collection, movie.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["BooksForMovie"] as SelectList;
            Assert.IsNotNull(selectList);

            var bookTitles = from item in selectList
                             select item.Text;

            Assert.AreEqual(2, selectList.Count<SelectListItem>());
            Assert.IsTrue(bookTitles.Contains<String>("Your Book"));
            Assert.IsTrue(bookTitles.Contains<String>("My Book"));
        }

        [TestMethod]
        public void FindBooksByTitleWithoutBookMatchesShouldReturnAddBookForMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book1 = TestUtil.generateBook();
            book1.Title = "Nothing";
            book1.OriginalTitle = "Nothing";
            AddMedia(book1);
            Book book2 = TestUtil.generateBook();
            book2.Title = "FUCK";
            book2.OriginalTitle = "FUCK";
            AddMedia(book2);
            Book book3 = TestUtil.generateBook();
            book3.Title = "GER";
            book3.OriginalTitle = "GER";
            AddMedia(book3);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["SearchBookBar"] = "Book";

            ViewResult result = mediaController.FindBooksByTitle(collection, movie.Id) as ViewResult;
            Assert.IsNotNull(result);

            SelectList selectList = result.ViewData["BooksForMovie"] as SelectList;
            Assert.IsNotNull(selectList);

            Assert.AreEqual(0, selectList.Count<SelectListItem>());
        }

        [TestMethod]
        public void FindBooksByTitleShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();
            FormCollection collection = new FormCollection();
            collection["SearchBookBar"] = "Book";

            ViewResult result = mediaController.FindBooksByTitle(collection, -1) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void AddBookToMovieShouldConnectAndReturnMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["BooksForMovie"] = book.Id.ToString();

            RedirectToRouteResult result = mediaController.AddBookToMovie(collection, movie.Id) as RedirectToRouteResult;

            Assert.AreEqual(book.Id, movie.Book.Id);
            Assert.AreEqual(movie.Id, book.Movie.Id);
            Assert.IsNotNull(result);
            object movieId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out movieId));
            Assert.AreEqual(movie.Id, (int)movieId);
        }

        [TestMethod]
        public void AddBookToMovieWithFalseNumberFormatShouldReturnNotFoundView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["BooksForMovie"] = "xx";

            ViewResult result = mediaController.AddBookToMovie(collection, movie.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void AddBookToMovieWithNoMovieShouldReturnNotFoundView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["BooksForMovie"] = book.Id.ToString();

            ViewResult result = mediaController.AddBookToMovie(collection, -1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void AddBookToMovieWithNoBookShouldReturnNotFoundView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            AddMedia(book);

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();
            collection["BooksForMovie"] = "-1";

            ViewResult result = mediaController.AddBookToMovie(collection, movie.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteBookFromMovieConnShouldDeleteConnAndReturnMovieView()
        {
            Movie movie = TestUtil.generateMovie();
            movie.Title = "Jurassic Park";
            movie.OriginalTitle = "Jurassic Park";
            AddMedia(movie);

            Book book = TestUtil.generateBook();
            book.Title = "Jurassic Park";
            book.OriginalTitle = "Jurassic Park";
            book.Movie = movie;
            AddMedia(book);

            MediaController mediaController = new MediaController();

            RedirectToRouteResult result = mediaController.DeleteBookFromMovieConn(movie.Id) as RedirectToRouteResult;

            Assert.IsNull(movie.Book);
            Assert.IsNull(book.Movie);
            Assert.IsNotNull(result);
            object movieId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out movieId));
            Assert.AreEqual(movie.Id, (int)movieId);
        }

        [TestMethod]
        public void DeleteBookFromMovieConnWithNoMovieShouldReturnNotFoundView()
        {
            MediaController mediaController = new MediaController();

            ViewResult result = mediaController.DeleteBookFromMovieConn(-1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}
