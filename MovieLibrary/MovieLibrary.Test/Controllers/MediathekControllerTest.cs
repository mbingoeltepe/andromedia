using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System.Security.Principal;

namespace MovieLibrary.Test.Controllers
{
    [TestClass]
    public class MediathekControllerTest
    {
        protected static UserBook userBook;
        protected static UserMovie userMovie;
        protected static UserTV_Show userTV_Show;
        protected static User user;
        protected static IPrincipal fakeUser;
        protected static IUserMediaService userMediaService;
        protected static MockRepository mocks = new MockRepository();

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            userBook = TestUtil.generateUserBook();
            userBook.Book.Id = 4;
            userBook.Id = 1;

            userMovie = TestUtil.generateUserMovie();
            userMovie.Movie.Id = 5;
            userMovie.Id = 2;

            userTV_Show = TestUtil.generateUserTV_Show();
            userTV_Show.Season.Id = 6;
            userTV_Show.Id = 3;

            user = new User();
            user.Username = "test@test.com";

            userBook.User = user;
            userMovie.User = user;
            userTV_Show.User = user;

            fakeUser = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null); 
        }

        [TestInitialize]
        public void TestInitialize()
        {
            userMediaService = mocks.StrictMock<IUserMediaService>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mocks.VerifyAll();
        }

        [TestMethod]
        public void DeleteBookShouldReturnRedirectToResult()
        {
            Expect.Call(userMediaService.GetBookById(userBook.Id)).Return(userBook);
            Expect.Call(delegate { userMediaService.DeleteBook(userBook); });
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            RedirectToRouteResult result = mediathekController.DeleteBook(userBook.Id, fakeUser) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            object bookId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out bookId));
            Assert.AreEqual(userBook.Book.Id, (int) bookId);
        }

        [TestMethod]
        public void DeleteBookShouldReturnNotFoundView()
        {
            Expect.Call(userMediaService.GetBookById(999)).Return(null);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            ViewResult result = mediathekController.DeleteBook(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteBookShouldReturnNotAuthorizedView()
        {
            Expect.Call(userMediaService.GetBookById(userBook.Id)).Return(userBook);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteBook(userBook.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }

        [TestMethod]
        public void DeleteMovieShouldReturnRedirectToResult()
        {
            Expect.Call(userMediaService.GetMovieById(userMovie.Id)).Return(userMovie);
            Expect.Call(delegate { userMediaService.DeleteMovie(userMovie); });
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            RedirectToRouteResult result = mediathekController.DeleteMovie(userMovie.Id, fakeUser) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            object movieId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out movieId));
            Assert.AreEqual(userMovie.Movie.Id, (int) movieId);
        }
        
        [TestMethod]
        public void DeleteMovieShouldReturnNotFoundView()
        {
            Expect.Call(userMediaService.GetMovieById(999)).Return(null);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            ViewResult result = mediathekController.DeleteMovie(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }
        
        [TestMethod]
        public void DeleteMovieShouldReturnNotAuthorizedView()
        {
            Expect.Call(userMediaService.GetMovieById(userMovie.Id)).Return(userMovie);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteMovie(userMovie.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }

        [TestMethod]
        public void DeleteTvShowShouldReturnRedirectToResult()
        {
            Expect.Call(userMediaService.GetTvShowById(userTV_Show.Id)).Return(userTV_Show);
            Expect.Call(delegate { userMediaService.DeleteTvShow(userTV_Show); });
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            RedirectToRouteResult result = mediathekController.DeleteTvShow(userTV_Show.Id, fakeUser) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            object tv_ShowId;
            Assert.IsTrue(result.RouteValues.TryGetValue("id", out tv_ShowId));
            Assert.AreEqual(userTV_Show.Season.TV_Show.Id, (int) tv_ShowId);
        }

        [TestMethod]
        public void DeleteTvShowShouldReturnNotFoundView()
        {
            Expect.Call(userMediaService.GetTvShowById(999)).Return(null);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            ViewResult result = mediathekController.DeleteTvShow(999, fakeUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteTvShowShouldReturnNotAuthorizedView()
        {
            Expect.Call(userMediaService.GetTvShowById(userTV_Show.Id)).Return(userTV_Show);
            mocks.ReplayAll();

            MediathekController mediathekController = new MediathekController(userMediaService);

            IPrincipal notOwner = new GenericPrincipal(new GenericIdentity("notOwner", "Forms"), null);

            ViewResult result = mediathekController.DeleteTvShow(userTV_Show.Id, notOwner) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("NotAuthorized", result.ViewName);
        }
    }
}
