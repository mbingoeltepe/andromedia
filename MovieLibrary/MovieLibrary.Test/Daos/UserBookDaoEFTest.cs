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
    public class UserBookDaoEFTest : AbstractUserMediaDaoEFTest<UserBook>
    {
        private UserBookDaoEF userBookDao = UserBookDaoEF.Instance;

        protected override UserBook generateUserMedia()
        {
            return TestUtil.generateUserBook();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
            userMediaDaoEF = UserBookDaoEF.Instance;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestMethod]
        public void GetUserBooksByBookIdShouldReturnThreeUserBooks()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();
            Book book = TestUtil.generateBook();

            UserBook userBook1 = TestUtil.generateUserBook(book);
            userBook1.User = user;
            AddUserMedia(userBook1);
            UserBook userBook2 = TestUtil.generateUserBook(book);
            userBook2.User = user;
            AddUserMedia(userBook2);
            UserBook userBook3 = TestUtil.generateUserBook(book);
            userBook3.User = user;
            AddUserMedia(userBook3);

            IQueryable<UserBook> userBooks = userBookDao.GetUserBooksByBookId(user.Username, book.Id);

            Assert.AreEqual(3, userBooks.Count<UserBook>());
        }

        [TestMethod]
        public void GetUserBooksByBookIdShouldReturnNoUserBooks()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();
            Book book = TestUtil.generateBook();

            IQueryable<UserBook> userBooks = userBookDao.GetUserBooksByBookId(user.Username, book.Id);

            Assert.AreEqual(0, userBooks.Count<UserBook>());
        }

        [TestMethod]
        public void GetUserBooksByBookIdShouldReturnNoUserBooksBecauseNoSuchUsername()
        {
            Book book = TestUtil.generateBook();

            IQueryable<UserBook> userBooks = userBookDao.GetUserBooksByBookId("nosuchusername", book.Id);

            Assert.AreEqual(0, userBooks.Count<UserBook>());
        }
    }
}