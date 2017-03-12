using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public abstract class AbstractUserMediaDaoEFTest<T> where T : UserMedia
    {
        protected static List<T> addedUserMedia = new List<T>();
        protected static AbstractUserMediaDaoEF<T> userMediaDaoEF;
        protected const int COUNT = 5;

        protected abstract T generateUserMedia();

        protected void AddUserMedia(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                T userMedia = generateUserMedia();
                userMedia.User = context.UserSet.First<User>();
                context.UserMediaSet.AddObject(userMedia);
                addedUserMedia.Add(userMedia);
            }
            context.SaveChanges();
        }

        protected void DeleteUserMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (T userMedia in addedUserMedia)
            {
                context.UserMediaSet.DeleteObject(userMedia);
            }
            context.SaveChanges();

            addedUserMedia.Clear();
        }

        protected void AddUserMedia(T quote)
        {
            userMediaDaoEF.Add(quote);
            addedUserMedia.Add(quote);
        }

        protected void DeleteUserMedia(T quote)
        {
            userMediaDaoEF.Delete(quote);
            addedUserMedia.Remove(quote);
        }

        protected static void CreateTestUser()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                User user = new User();
                user.Username = "test@email.com";
                user.Password = "pwd";

                context.UserSet.AddObject(user);
                context.SaveChanges();
            }
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();

            AddUserMedia(COUNT);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            DeleteUserMedia();
        }
        
        [TestMethod()]
        public void GetAllShouldReturnFiveUserMedia()
        {
            IQueryable<T> userMedia = userMediaDaoEF.GetAll();
            Assert.AreEqual(addedUserMedia.Count, userMedia.Count<T>());
        }
        
        [TestMethod()]
        public void GetAllShouldReturnNoUserMedia()
        {
            DeleteUserMedia();
            IQueryable<T> userMedia = userMediaDaoEF.GetAll();
            Assert.AreEqual(0, userMedia.Count<T>());
        }
        
        [TestMethod()]
        public void GetByIdShouldReturnUserMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            T userMedia_expected = context.UserMediaSet.OfType<T>().First<T>();
            T userMedia = userMediaDaoEF.GetById(userMedia_expected.Id);

            Assert.AreEqual(userMedia.Id, userMedia_expected.Id);
        }

        [TestMethod()]
        public void GetByIdShouldReturnNull()
        {
            Assert.IsNull(userMediaDaoEF.GetById(-1));
        }
        
        [TestMethod()]
        public void AddShouldPersistUserMedia()
        {
            int count_before = userMediaDaoEF.GetAll().Count<T>();

            T userMedia = generateUserMedia();

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            userMedia.User = context.UserSet.First<User>();

            AddUserMedia(userMedia);

            int count_after = userMediaDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before + 1, count_after);
        }

        [TestMethod()]
        public void DeleteShouldDeleteUserMedia()
        {
            T userMedia = addedUserMedia.Last<T>();

            int count_before = userMediaDaoEF.GetAll().Count<T>();

            DeleteUserMedia(userMedia);

            int count_after = userMediaDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before - 1, count_after);
        }

        [TestMethod()]
        public void UpdateShouldSaveChanges()
        {
            T expected = addedUserMedia.Last<T>();

            expected.MediaStatus = "Verborgt";

            userMediaDaoEF.Save();

            T actual = userMediaDaoEF.GetById(expected.Id);

            Assert.AreEqual(expected.MediaStatus, actual.MediaStatus);
        }
    }
}
