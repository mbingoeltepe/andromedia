using MovieLibrary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using Rhino.Mocks;
using MovieLibrary.Daos;
using MovieLibrary.Daos.Interfaces;
using System.Collections.Generic;
using MovieLibrary.Models;
using System.Linq;
using MovieLibrary.Helpers;
using MovieLibrary.Daos.EntityFramework;
using System.Web;
using System.Web.Hosting;
using System.IO;
using MovieLibrary.Service.IServices;

namespace MovieLibrary.Test
{
    [TestClass()]
    public class QuoteControllerTest
    {
        protected static List<Quote> quotes = new List<Quote>();
        protected static IQuoteService<Quote> quotesDao;
        protected static MockRepository mocks = new MockRepository();
        protected const int COUNT = 27;

        protected void AddQuotes(int counter)
        {
            for (int i = 1; i <= counter; i++)
            {
                Quote quote = TestUtil.generateQuoteMovie();
                quotes.Add(quote);
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            AddQuotes(COUNT);
            quotesDao = mocks.StrictMock<IQuoteService<Quote>>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mocks.VerifyAll();
            quotes.Clear();
        }

        [TestMethod]
        public void TopQuotesShouldReturnExpectedView()
        {
            Expect.Call(quotesDao.GetAllQuotes()).Return(quotes.AsQueryable<Quote>());
            mocks.ReplayAll();

            QuoteController quoteController = new QuoteController(quotesDao);
            ViewResult result = quoteController.TopQuotes() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("TopQuotes", result.ViewName);
        }

        [TestMethod]
        public void TopQuotesShouldReturnExpectedViewData()
        {
            Expect.Call(quotesDao.GetAllQuotes()).Return(quotes.AsQueryable<Quote>());
            mocks.ReplayAll();

            QuoteController quoteController = new QuoteController(quotesDao);
            ViewResult result = quoteController.TopQuotes() as ViewResult;
            PaginatedTopQuotesList actual = result.ViewData.Model as PaginatedTopQuotesList;

            Assert.IsNotNull(actual);
            Assert.AreEqual(quoteController.pageSize, actual.Count());
        }
    }
}