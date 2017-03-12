using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using MovieLibrary.Service.ServicesImpl;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class MedienAusborgenIntTest
    {

        protected static List<UserMedia> addedUserMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_show;
        protected static UserBook book;
        protected static User user;
        protected static IPrincipal fakeUserFrom;
        protected static IPrincipal fakeUserTo;

        protected void AddBooks(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserBook book = TestUtil.generateUserBook();
                context.UserMediaSet.AddObject(book);
            }
            context.SaveChanges();
        }

        
        protected void DeleteMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            List<BorrowedDetails> details = new List<BorrowedDetails>();
            List<BorrowRequest> request = new List<BorrowRequest>();

            foreach (UserMedia userMedia in addedUserMedia)
            {
                foreach (var detail in userMedia.BorrowedDetails)
                {
                    details.Add(detail);
                }
                foreach (var rq in userMedia.BorrowRequest)
                {
                    request.Add(rq);
                }
                context.UserMediaSet.DeleteObject(userMedia);
            }
            foreach (var detail in details)
            {
                context.BorrowedDetailsSet.DeleteObject(detail);
            }
            foreach (var rq in request)
            {
                context.BorrowRequestSet.DeleteObject(rq);
            }
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
                
                book = TestUtil.generateUserBook();

                context.UserSet.AddObject(user);
                context.UserMediaSet.AddObject(book);
                context.SaveChanges();
            }

            fakeUserFrom = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null);
            fakeUserTo = new GenericPrincipal(new GenericIdentity("testTo@test.test", "Forms"), null);
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
            CreateTestContext();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteMedia();
        }


        [TestMethod]
        public void GiveBorrowedMediaBackRequestTest()
        {
            //AddBooks(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserBook book = context.UserMediaSet.OfType<UserBook>().First<UserBook>();
            int expectedBookId = book.Id;
            MediathekController mediathekController = new MediathekController(); 
            
            mediathekController.BorrowMediaToUser(fakeUserTo.Identity.Name, book.Id, fakeUserFrom, null);

            RedirectToRouteResult result = mediathekController.GiveBorrowedMediaBackRequest(book.Id, fakeUserTo);

            IQueryable<UserBook> userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(fakeUserFrom.Identity.Name, book.Id);

            addedUserMedia.Add(userBooks.Single<UserBook>());

            Assert.IsNotNull(userBooks);
            Assert.AreEqual(expectedBookId, userBooks.Single<UserBook>().Id);
            Assert.AreEqual(1, userBooks.Single<UserBook>().BorrowedDetails.Count);
            foreach (var detail in userBooks.Single<UserBook>().BorrowedDetails)
            {
                Assert.AreEqual(true, detail.TakeBackRequest); 
            }
        }

        [TestMethod]
        public void TestSendRequestToBorrow()
        {           

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserBook book = context.UserMediaSet.OfType<UserBook>().First<UserBook>();
            int expectedBookId = book.Id;
            MediathekController mediathekController = new MediathekController();           

            RedirectToRouteResult result = mediathekController.SendBorrowRequest(book.Id, fakeUserTo);

            IQueryable<UserBook> userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(fakeUserFrom.Identity.Name, book.Id);

            addedUserMedia.Add(userBooks.Single<UserBook>());

            Assert.IsNotNull(userBooks);
            Assert.AreEqual(expectedBookId, userBooks.Single<UserBook>().Id);
            Assert.AreEqual(userBooks.Single<UserBook>().BorrowRequest.Single().User.Username, fakeUserFrom.Identity.Name);
        }

        [TestMethod]
        public void TestBorrowMediaToUser()
        {

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            UserBook book = context.UserMediaSet.OfType<UserBook>().First<UserBook>();
            int expectedBookId = book.Id;
            MediathekController mediathekController = new MediathekController();

            FormCollection collection = new FormCollection();

            collection["User.Friends"] = fakeUserTo.Identity.Name;

            RedirectToRouteResult result = mediathekController.BorrowMediaToUser(fakeUserTo.Identity.Name,book.Id,fakeUserTo,collection);

            IQueryable<UserBook> userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(fakeUserFrom.Identity.Name, book.Id);

            addedUserMedia.Add(userBooks.Single<UserBook>());

            Assert.IsNotNull(userBooks);
            Assert.AreEqual(expectedBookId, userBooks.Single<UserBook>().Id);
            Assert.AreEqual(userBooks.Single<UserBook>().BorrowedDetails.Single().NameTo, fakeUserTo.Identity.Name);
        }

    }
}
