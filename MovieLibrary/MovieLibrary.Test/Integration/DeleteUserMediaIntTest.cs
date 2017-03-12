using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using System.Security.Principal;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class DeleteUserMediaIntTest
    {
        protected static List<UserMedia> addedUserMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_Show;
        protected static Book book;
        protected static User user;
        protected static IPrincipal fakeUser;
        protected static IUserMediaService userMediaService = UserMediaService.Instance;

        protected void AddUserBooks(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
                for (int i = 1; i <= count; i++)
                {
                    UserBook userBook = TestUtil.generateUserBook();
                    userBook.User = context.UserSet.First<User>();
                    userBook.Book = context.MediaSet.OfType<Book>().First<Book>();
                    context.UserMediaSet.AddObject(userBook);
                    addedUserMedia.Add(userBook);
                }
                context.SaveChanges();
        }

        protected void AddUserMovies(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserMovie userMovie = TestUtil.generateUserMovie();
                userMovie.User = context.UserSet.First<User>();
                userMovie.Movie = context.MediaSet.OfType<Movie>().First<Movie>();
                context.UserMediaSet.AddObject(userMovie);
                addedUserMedia.Add(userMovie);
            }
            context.SaveChanges();
        }

        protected void AddUserTV_Shows(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserTV_Show userTV_Show = TestUtil.generateUserTV_Show();
                userTV_Show.User = context.UserSet.First<User>();
                userTV_Show.Season.TV_Show = context.MediaSet.OfType<TV_Show>().First<TV_Show>();
                context.UserMediaSet.AddObject(userTV_Show);
                addedUserMedia.Add(userTV_Show);
            }
            context.SaveChanges();
        }

        protected void DeleteUserMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (UserMedia userMedia in addedUserMedia)
            {
                context.UserMediaSet.DeleteObject(userMedia);
            }
            context.SaveChanges();

            addedUserMedia.Clear();
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
                tv_Show = TestUtil.generateTV_Show(false);

                context.UserSet.AddObject(user);
                context.MediaSet.AddObject(book);
                context.MediaSet.AddObject(movie);
                context.MediaSet.AddObject(tv_Show);
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
            DeleteUserMedia();
        }
        
        [TestMethod]
        public void DeleteBookShouldReturnRedirectToResult()
        {
            AddUserBooks(3);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserBook userBook = context.UserMediaSet.OfType<UserBook>().First<UserBook>();
            int expectedBookId = userBook.Book.Id;

            MediathekController mediathekController = new MediathekController();

            RedirectToRouteResult result = mediathekController.DeleteBook(userBook.Id, fakeUser) as RedirectToRouteResult;
            addedUserMedia.Remove(userBook);

            Assert.IsNotNull(result);
            object bookId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out bookId));
            Assert.AreEqual(expectedBookId, (int) bookId);
        }
        
        [TestMethod]
        public void DeleteBookShouldReturnNotFoundView()
        {
            MediathekController mediathekController = new MediathekController();

            ViewResult result = mediathekController.DeleteBook(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
        
        [TestMethod]
        public void DeleteBookShouldReturnNotAuthorizedView()
        {
            AddUserBooks(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserBook userBook = context.UserMediaSet.OfType<UserBook>().First<UserBook>();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteBook(userBook.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }
        
        [TestMethod]
        public void DeleteMovieShouldReturnRedirectToResult()
        {
            AddUserMovies(3);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserMovie userMovie = context.UserMediaSet.OfType<UserMovie>().First<UserMovie>();
            int expectedMovieId = userMovie.Movie.Id;

            MediathekController mediathekController = new MediathekController();

            RedirectToRouteResult result = mediathekController.DeleteMovie(userMovie.Id, fakeUser) as RedirectToRouteResult;
            addedUserMedia.Remove(userMovie);

            Assert.IsNotNull(result);
            object movieId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out movieId));
            Assert.AreEqual(expectedMovieId, (int)movieId);
        }
        
        [TestMethod]
        public void DeleteMovieShouldReturnNotFoundView()
        {
            MediathekController mediathekController = new MediathekController(userMediaService);

            ViewResult result = mediathekController.DeleteMovie(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteMovieShouldReturnNotAuthorizedView()
        {
            AddUserMovies(2);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserMovie userMovie = context.UserMediaSet.OfType<UserMovie>().First<UserMovie>();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteMovie(userMovie.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }

                
        [TestMethod]
        public void DeleteTvShowShouldReturnRedirectToResult()
        {
            AddUserTV_Shows(3);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserTV_Show userTV_Show = context.UserMediaSet.OfType<UserTV_Show>().First<UserTV_Show>();
            int expectedTV_ShowId = userTV_Show.Season.TV_Show.Id;

            MediathekController mediathekController = new MediathekController();

            RedirectToRouteResult result = mediathekController.DeleteTvShow(userTV_Show.Id, fakeUser) as RedirectToRouteResult;
            addedUserMedia.Remove(userTV_Show);

            Assert.IsNotNull(result);
            object tv_ShowId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out tv_ShowId));
            Assert.AreEqual(expectedTV_ShowId, (int) tv_ShowId);
        }
        
        [TestMethod]
        public void DeleteTvShowShouldReturnNotFoundView()
        {
            MediathekController mediathekController = new MediathekController(userMediaService);

            ViewResult result = mediathekController.DeleteTvShow(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteTvShowShouldReturnNotAuthorizedView()
        {
            AddUserTV_Shows(2);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserTV_Show userTV_Show = context.UserMediaSet.OfType<UserTV_Show>().First<UserTV_Show>();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteTvShow(userTV_Show.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }
    }
}
