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
using System;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class UserLoeschenIntTest
    {
        protected static User admin;
        protected static User user1;
        protected static User user2;
        protected static IPrincipal fakeAdmin;

       
        protected static void CreateTestContext()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                admin = new User();
                admin.Username = "admin@andromedia.com";
                admin.Password = "admin";
                admin.Verified = true;
                admin.Closed = false;

                user1 = new User();
                user1.Username = "test@test.com";
                user1.Password = "test";
                user1.Verified = true;
                user1.Closed = false;

                user2 = new User();
                user2.Username = "other@test.com";
                user2.Password = "other";
                user2.Verified = true;
                user2.Closed = false;


                context.UserSet.AddObject(admin);
                context.UserSet.AddObject(user1);
                context.UserSet.AddObject(user2);
                context.SaveChanges();
            }

            fakeAdmin = new GenericPrincipal(new GenericIdentity(admin.Username, "Forms"), null);
        }

        protected static void DeleteUsers()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (User user in context.UserSet)
            {
                context.UserSet.DeleteObject(user);
            }
            context.SaveChanges();
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
        public void TestCleanup()
        {
            DeleteUsers();
        }


        [TestMethod()]
        public void TestViewName()
        {
            var controller = new AdminController();
            var result = controller.ShowUsers() as ViewResult;

            Assert.AreEqual("ShowUsers", result.ViewName);

        }

        [TestMethod()]
        public void TestUserListSize()
        {

            var controller = new AdminController();

            var result = controller.ShowUsers() as ViewResult;
            var produkt = (IQueryable<User>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 2);


        }

        [TestMethod()]
        public void TestUserFiltern()
        {

            var controller = new AdminController();

            FormCollection collection = new FormCollection();
            collection["NameBox"] = "test";

            var result = controller.FilternUsers(collection, fakeAdmin) as PartialViewResult;
            var produkt = (IQueryable<User>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 1);


        }

        [TestMethod()]
        public void TestUserLoeschen()
        {

            var controller = new AdminController();
            // Zuerst wird der User gefunden.
            FormCollection collection = new FormCollection();
            collection["NameBox"] = "test@test.com";

            var resultFiltern = controller.FilternUsers(collection, fakeAdmin) as PartialViewResult;
            var produktFiltern = (IQueryable<User>)resultFiltern.ViewData.Model;

            int userId = (int)produktFiltern.Single().Id;

            // Und wird dieser User gelöscht;

            var resultLoschen = controller.UserLoeschen(userId, fakeAdmin) as ViewResult;
            var produktLoschen = (IQueryable<User>)resultLoschen.ViewData.Model;

            // Jetzt gibt es in der DB 4 Users.

            var result = controller.ShowUsers() as ViewResult;
            var produkt = (IQueryable<User>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 2);

        }
     

        
    }
}
