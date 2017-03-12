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
    public class ZitatRankenIntTest
    {
        protected static User admin;
        protected static IPrincipal fakeAdmin;

        protected static void CreateTestContext()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                admin = new User();
                admin.Username = "admin@andromedia.com";
                admin.Password = "admin";

                context.UserSet.AddObject(admin);
                context.SaveChanges();
            }

            fakeAdmin = new GenericPrincipal(new GenericIdentity(admin.Username, "Forms"), null);
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            TestUtil.InsertTestData();
            // In der DB wird folgende Quotes mit Rankin=0 hinzugefügt
            // insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId, Ranking)
            // values ('DummyQuote', 'The Terminator', 'English', 1, 1, 1, 0);
            // values ('DummyQuote1', 'The Terminator', 'English', 1, 1, 2, 0);
            // values ('DummyQuote2', 'The Terminator', 'English', 1, 2, 3, 0);
            // values ('DummyQuote3', 'The Terminator', 'English', 1, 3, 3, 0);
            // values ('DummyQuote3', 'The Terminator', 'English', 1, 3, 4, 0);            

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


        [TestMethod()]
        public void TestViewName()
        {
            var controller = new QuoteController();
            var result = controller.ShowNotRankingZitate() as ViewResult;

            Assert.AreEqual("ShowAllNotRankingQuotes", result.ViewName);

        }

        [TestMethod()]
        public void TestQuoteListSize()
        {

            var controller = new QuoteController();

            var result = controller.ShowNotRankingZitate() as ViewResult;
            var produkt = (IQueryable<Quote>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 5);


        }

        [TestMethod()]
        public void TestWithUserNameFiltern()
        {

            var controller = new QuoteController();

            FormCollection collection = new FormCollection();
            collection["NotRakingUserList"] = "test@test.com";
            collection["NotRakingLanguage"] = "";
            collection["NotRakingMediumTitle"] = "";

            var result = controller.FilternQuotes(collection) as PartialViewResult;
            var produkt = (IQueryable<Quote>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 2);
        }

        [TestMethod()]
        public void TestWithSpracheFiltern()
        {

            var controller = new QuoteController();

            FormCollection collection = new FormCollection();
            collection["NotRakingUserList"] = "";
            collection["NotRakingLanguage"] = Models.QuoteLanguageEnum.English.ToString();
            collection["NotRakingMediumTitle"] = "";

            var result = controller.FilternQuotes(collection) as PartialViewResult;
            var produkt = (IQueryable<Quote>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 5);
        }

        [TestMethod()]
        public void TestQuoteWithTitleFiltern()
        {

            var controller = new QuoteController();

            FormCollection collection = new FormCollection();
            collection["NotRakingUserList"] = "";
            collection["NotRakingLanguage"] = "";
            collection["NotRakingMediumTitle"] = "Der Terminator";

            var result = controller.FilternQuotes(collection) as PartialViewResult;
            var produkt = (IQueryable<Quote>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 1);
        }

        [TestMethod()]
        public void TestNotRankingQuoteWirdGerankt()
        {

            var controller = new QuoteController();
            // Zuerst wird Zitat gefunden.

            FormCollection collection = new FormCollection();
            collection["NotRakingUserList"] = "testEins@test.com";
            collection["NotRakingLanguage"] = "";
            collection["NotRakingMediumTitle"] = "";

            var resultFiltern = controller.FilternQuotes(collection) as PartialViewResult;
            var produktFiltern = (IQueryable<Quote>)resultFiltern.ViewData.Model;

            int quoteId = (int)produktFiltern.Single().Id;
            //Und wird diese Zitat gerankt.

            collection["RankenWert"] = "3";
            collection["Rolle"] = "The Terminator";
            collection["Language"] = QuoteLanguageEnum.English.ToString();
            collection["QuoteString"] = "DummyQuote";
            var resultRanken = controller.ZitatRanken(quoteId, collection ,fakeAdmin) as ViewResult;
            var produktRanken = (IQueryable<Quote>)resultRanken.ViewData.Model;

            // Jetzt gibt es in der DB 4 Zitate die noch niht gerankt sind.

            var result = controller.ShowNotRankingZitate() as ViewResult;
            var produkt = (IQueryable<Quote>)result.ViewData.Model;


            Assert.AreEqual(produkt.Count(), 4);
        }

    }
}
