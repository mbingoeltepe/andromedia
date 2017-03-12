using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public class UserMovieDaoEFTest : AbstractUserMediaDaoEFTest<UserMovie>
    {
        private UserMovieDaoEF userMovieDao = UserMovieDaoEF.Instance;

        protected override UserMovie generateUserMedia()
        {
            return TestUtil.generateUserMovie();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
            userMediaDaoEF = UserMovieDaoEF.Instance;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestMethod]
        public void GetUserVideoByVideoIdShouldReturnThreeUserVideos()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();
            Movie movie = TestUtil.generateMovie();

            UserMovie userMovie1 = TestUtil.generateUserMovie(movie);
            userMovie1.User = user;
            AddUserMedia(userMovie1);
            UserMovie userMovie2 = TestUtil.generateUserMovie(movie);
            userMovie2.User = user;
            AddUserMedia(userMovie2);
            UserMovie userMovie3 = TestUtil.generateUserMovie(movie);
            userMovie3.User = user;
            AddUserMedia(userMovie3);

            IQueryable<UserMovie> userMovies = userMovieDao.GetUserMovieByMovieId(user.Username, movie.Id);

            Assert.AreEqual(3, userMovies.Count<UserMovie>());
        }

        [TestMethod]
        public void GetUserVideoByVideoIdShouldReturnNoUserVideos()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();
            Movie movie = TestUtil.generateMovie();

            IQueryable<UserMovie> userMovies = userMovieDao.GetUserMovieByMovieId(user.Username, movie.Id);

            Assert.AreEqual(0, userMovies.Count<UserMovie>());
        }

        [TestMethod]
        public void GetUserVideoByVideoIdShouldReturnNoUserVideosBecauseNoSuchUsername()
        {
            Movie movie = TestUtil.generateMovie();

            IQueryable<UserMovie> userMovies = userMovieDao.GetUserMovieByMovieId("nosuchusername", movie.Id);

            Assert.AreEqual(0, userMovies.Count<UserMovie>());
        }
    }
}
