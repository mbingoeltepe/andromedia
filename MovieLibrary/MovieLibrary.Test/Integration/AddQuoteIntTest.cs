using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Helpers;
using MovieLibrary.Models;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using System;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class AddQuoteIntTest
    {
        protected static List<UserMedia> addedMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_show;
        protected static Book book;
        protected static User user;
        protected static IPrincipal fakeUser;
        

        protected void AddBooks(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                Book book = TestUtil.generateBook();
                context.MediaSet.AddObject(book);
            }
            context.SaveChanges();
        }

        protected void AddMovies(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                Movie movie = TestUtil.generateMovie();
                context.MediaSet.AddObject(movie);
            }
            context.SaveChanges();
        }

        protected void AddTvShows(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                TV_Show tvShows = TestUtil.generateTV_Show(true);
                context.MediaSet.AddObject(tvShows);
            }
            context.SaveChanges();
        }

        protected void DeleteMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

  
            foreach (UserMedia media in addedMedia)
            {
                context.UserMediaSet.DeleteObject(media);
            }
            context.SaveChanges();

            addedMedia.Clear();
        }

        protected static void CreateTestContext()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                user = new User();
                user.Username = "test@test.com";
                user.Password = "pwd";

                book = TestUtil.generateBook();
                movie = TestUtil.generateMovie();
                tv_show = TestUtil.generateTV_Show(true);

                context.UserSet.AddObject(user);
                context.MediaSet.AddObject(book);
                context.MediaSet.AddObject(movie);
                context.MediaSet.AddObject(tv_show);
                context.SaveChanges();
            }

            fakeUser = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null);
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestContext();

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
        public void TestAddQuoteToBook()
        {
            AddBooks(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Book book = context.MediaSet.OfType<Book>().First<Book>();
            int expectedBookId = book.Id;

            QuoteController quoteController = new QuoteController();

            
            FormCollection collection = new FormCollection();

            collection["Language"] = QuoteLanguageEnum.Deutsch.ToString();
            collection["Character"] = "Rolle";
            collection["QuoteString"] = "QuoteStringBook";
            

            ViewResult result = quoteController.AddQuote(book.Id, collection, fakeUser) as ViewResult;

            Book books = MediaService.Instance.GetBookById(book.Id);

            
            Assert.IsNotNull(books);

            Assert.AreEqual(collection["Language"], books.Quote.Single<Quote>().Language);
            Assert.AreEqual(collection["Character"], books.Quote.Single<Quote>().Character);
            Assert.AreEqual(collection["QuoteString"], books.Quote.Single<Quote>().QuoteString);

            Assert.AreEqual("AddQuoteMessage", result.ViewName);

        }

        [TestMethod]
        public void TestAddQuoteToMovie()
        {
            AddMovies(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Movie movie = context.MediaSet.OfType<Movie>().First<Movie>();
            int expectedMovieId = movie.Id;

            QuoteController quoteController = new QuoteController();


            FormCollection collection = new FormCollection();

            collection["Language"] = QuoteLanguageEnum.Deutsch.ToString();
            collection["Character"] = "Rolle";
            collection["Wann"] = "01:11:15";
            collection["QuoteString"] = "QuoteStringMovie";


            ViewResult result = quoteController.AddQuote(movie.Id, collection, fakeUser) as ViewResult;

            Movie movies = MediaService.Instance.GetMovieById(movie.Id);

            Assert.IsNotNull(movies);

            Assert.AreEqual(collection["Language"], movies.Quote.Single<Quote>().Language);
            Assert.AreEqual(collection["Character"], movies.Quote.Single<Quote>().Character);
            Assert.AreEqual(collection["Wann"], movies.Quote.Single<Quote>().OccurenceTime);
            Assert.AreEqual(collection["QuoteString"], movies.Quote.Single<Quote>().QuoteString);

            Assert.AreEqual("AddQuoteMessage", result.ViewName);

        }

        [TestMethod]
        public void TestAddQuoteToTvShow()
        {
            AddTvShows(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            TV_Show tvshow = context.MediaSet.OfType<TV_Show>().First<TV_Show>();
            int expectedTvShowId = tvshow.Id;

            QuoteController quoteController = new QuoteController();


            FormCollection collection = new FormCollection();

            collection["Language"] = QuoteLanguageEnum.Deutsch.ToString();
            collection["Character"] = "Rolle";
            collection["Wann"] = "00:11:09";
            collection["QuoteString"] = "QuoteStringTvShow";


            ViewResult result = quoteController.AddQuote(tvshow.Id, collection, fakeUser) as ViewResult;

            TV_Show tvshows = MediaService.Instance.GetTvShowById(tvshow.Id);

            Assert.IsNotNull(tvshows);

            Assert.AreEqual(collection["Language"], tvshows.Quote.Single<Quote>().Language);
            Assert.AreEqual(collection["Character"], tvshows.Quote.Single<Quote>().Character);
            Assert.AreEqual(collection["Wann"], tvshows.Quote.Single<Quote>().OccurenceTime);
            Assert.AreEqual(collection["QuoteString"], tvshows.Quote.Single<Quote>().QuoteString);

            Assert.AreEqual("AddQuoteMessage", result.ViewName);

        }
  

    }
}
