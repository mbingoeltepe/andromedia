using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Controllers;
using System.Web.Mvc;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class QuoteSearchIntTest
    {
        protected static List<Quote> addedQuotes = new List<Quote>();
        protected static string resultQuotesView = "ResultQuotes";

        protected void DeleteQuotes()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (Quote quote in addedQuotes)
            {
                context.QuoteSet.DeleteObject(quote);
            }
            context.SaveChanges();

            addedQuotes.Clear();
        }

        protected void AddQuote(Quote quote)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            quote.User = context.UserSet.First<User>();
            context.QuoteSet.AddObject(quote);
            context.SaveChanges();

            addedQuotes.Add(quote);
        }

        protected static void CreateTestUser()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                User user = new User();
                user.Username = "test@test.com";
                user.Password = "test";

                context.UserSet.AddObject(user);
                context.SaveChanges();
            }
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
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

            AddQuote(TestUtil.generateQuoteMovie());
            AddQuote(TestUtil.generateQuoteMovie());
            AddQuote(TestUtil.generateQuoteBook());
            AddQuote(TestUtil.generateQuoteBook());
            AddQuote(TestUtil.generateQuoteTV_Show());
            AddQuote(TestUtil.generateQuoteTV_Show());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteQuotes();
        }

        [TestMethod]
        public void ResultsShouldReturnQuotes()
        {
            QuoteTV_Show qtvshow = TestUtil.generateQuoteTV_Show();
            qtvshow.QuoteString = "bla search bla";
            AddQuote(qtvshow);

            SearchController searchController = new SearchController();

            FormCollection collection = new FormCollection();
            collection["TextBoxSearch"] = "search";

            ViewResult result = searchController.Results(collection) as ViewResult;
            Assert.IsNotNull(result);

            IEnumerable<Quote> model = result.ViewData.Model as IEnumerable<Quote>;
            Assert.IsNotNull(model);

            IEnumerable<string> quoteStrings = from quote in model
                                               select quote.QuoteString;

            Assert.IsTrue(quoteStrings.Contains(qtvshow.QuoteString));
            Assert.AreEqual(resultQuotesView, result.ViewName);
        }

        [TestMethod]
        public void ResultsShouldReturnReturnOrderedQuotesWithoutNonZeroQuotes()
        {
            DeleteQuotes();

            QuoteMovie quote0 = TestUtil.generateQuoteMovie();
            quote0.QuoteString = "Ranking 0 bla bla";
            quote0.Ranking = 0;
            AddQuote(quote0);

            QuoteBook quote1 = TestUtil.generateQuoteBook();
            quote1.QuoteString = "bla bla Ranking 1 bla bla";
            quote1.Ranking = 1;
            AddQuote(quote1);

            QuoteTV_Show quote2 = TestUtil.generateQuoteTV_Show();
            quote2.QuoteString = "bla Ranking 2 bla bla";
            quote2.Ranking = 2;
            AddQuote(quote2);

            QuoteBook quote3 = TestUtil.generateQuoteBook();
            quote3.QuoteString = "Ranking 3";
            quote3.Ranking = 3;
            AddQuote(quote3);

            SearchController searchController = new SearchController();

            FormCollection collection = new FormCollection();
            collection["TextBoxSearch"] = "Ranking";

            ViewResult result = searchController.Results(collection) as ViewResult;
            Assert.IsNotNull(result);

            IEnumerable<Quote> model = result.ViewData.Model as IEnumerable<Quote>;
            Assert.IsNotNull(model);

            Assert.AreEqual(3, model.Count<Quote>());

            int i = 1;
            foreach (Quote quote in model)
            {
                Assert.AreEqual(i++, quote.Ranking);
                Assert.IsTrue(quote.QuoteString.Contains("Ranking"));
            }

            Assert.AreEqual(resultQuotesView, result.ViewName);
        }

        [TestMethod]
        public void ResultsShouldReturnReturnNoQuotes()
        {
            DeleteQuotes();

            SearchController searchController = new SearchController();

            FormCollection collection = new FormCollection();
            collection["TextBoxSearch"] = "something";

            ViewResult result = searchController.Results(collection) as ViewResult;
            Assert.IsNotNull(result);

            IEnumerable<Quote> model = result.ViewData.Model as IEnumerable<Quote>;
            Assert.IsNotNull(model);

            Assert.AreEqual(0, model.Count<Quote>());

            Assert.AreEqual(resultQuotesView, result.ViewName);
        }
    }
}
