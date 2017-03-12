using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public class UserTvShowDaoEFTest : AbstractUserMediaDaoEFTest<UserTV_Show>
    {
        private UserTvShowDaoEF userTvShowDao = UserTvShowDaoEF.Instance;

        protected override UserTV_Show generateUserMedia()
        {
            return TestUtil.generateUserTV_Show();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
            userMediaDaoEF = UserTvShowDaoEF.Instance;
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
            TV_Show tv_Show = TestUtil.generateTV_Show(false);

            UserTV_Show userTV_Show1 = TestUtil.generateUserTV_Show(new Season { Number = 1, TV_Show = tv_Show });
            userTV_Show1.User = user;
            AddUserMedia(userTV_Show1);
            UserTV_Show userTV_Show2 = TestUtil.generateUserTV_Show(new Season { Number = 2, TV_Show = tv_Show });
            userTV_Show2.User = user;
            AddUserMedia(userTV_Show2);
            UserTV_Show userTV_Show3 = TestUtil.generateUserTV_Show(new Season { Number = 3, TV_Show = tv_Show });
            userTV_Show3.User = user;
            AddUserMedia(userTV_Show3);

            IQueryable<UserTV_Show> userTV_Shows = userTvShowDao.GetUserTvShowByTvShowId(user.Username, tv_Show.Id);

            Assert.AreEqual(3, userTV_Shows.Count<UserTV_Show>());
        }

        [TestMethod]
        public void GetUserVideoByVideoIdShouldReturnNoUserVideos()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();
            TV_Show tv_Show = TestUtil.generateTV_Show(false);

            IQueryable<UserTV_Show> userTV_Shows = userTvShowDao.GetUserTvShowByTvShowId(user.Username, tv_Show.Id);

            Assert.AreEqual(0, userTV_Shows.Count<UserTV_Show>());
        }

        [TestMethod]
        public void GetUserVideoByVideoIdShouldReturnNoUserVideosBecauseNoSuchUsername()
        {
            TV_Show tv_Show = TestUtil.generateTV_Show(false);

            IQueryable<UserTV_Show> userTV_Shows = userTvShowDao.GetUserTvShowByTvShowId("nosuchusername", tv_Show.Id);

            Assert.AreEqual(0, userTV_Shows.Count<UserTV_Show>());
        }
    }
}
