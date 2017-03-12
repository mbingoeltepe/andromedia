using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Data;
using MovieLibrary.Daos;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public abstract class AbstractQuoteDaoEFTest<T> where T : Quote
    {
        protected static List<T> addedQuotes = new List<T>();
        protected static AbstractQuoteDaoEF<T> quoteDaoEF;
        protected const int COUNT = 5;

        protected abstract T generateQuote();

        protected void AddQuotes(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
                for (int i = 1; i <= count; i++)
                {
                    T quote = generateQuote();
                    quote.User = context.UserSet.First<User>();
                    context.QuoteSet.AddObject(quote);
                    addedQuotes.Add(quote);
                }
                context.SaveChanges();
            
        }

        protected void DeleteQuotes()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
                foreach (T quote in addedQuotes)
                {
                    context.QuoteSet.DeleteObject(quote);
                }
                context.SaveChanges();
            
            addedQuotes.Clear();
        }

        protected void AddQuote(T quote)
        {
            quoteDaoEF.Add(quote);
            addedQuotes.Add(quote);
        }

        protected void DeleteQuote(T quote)
        {
            quoteDaoEF.Delete(quote);
            addedQuotes.Remove(quote);
        }

        protected void CreateDummyHttpContext()
        {
            HttpWorkerRequest request = new SimpleWorkerRequest("/dummy", @"c:\inetpub\wwwroot\dummy", "dummy.html", null, new StringWriter());
            HttpContext.Current = new HttpContext(request);
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
            CreateDummyHttpContext();

            AddQuotes(COUNT);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            DeleteQuotes();
        }
        
        [TestMethod()]
        public void GetAllShouldReturnFiveQuotes()
        {
            IQueryable<T> quotes = quoteDaoEF.GetAll();
            Assert.AreEqual(addedQuotes.Count, quotes.Count<T>());
        }
        
        [TestMethod()]
        public void GetAllShouldReturnNoQuotes()
        {
            DeleteQuotes();
            IQueryable<T> quotes = quoteDaoEF.GetAll();
            Assert.AreEqual(0, quotes.Count<T>());
        }
        
        [TestMethod()]
        public void GetByIdShouldReturnQuotes()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            T quote_expected = context.QuoteSet.OfType<T>().First<T>();
            T quote = quoteDaoEF.GetById(quote_expected.Id);

            Assert.AreEqual(quote.Id, quote_expected.Id);
        }
        
        [TestMethod()]
        public void GetByIdShouldReturnNull()
        {
            Assert.IsNull(quoteDaoEF.GetById(-1));
        }
        
        [TestMethod()]
        public void AddShouldPersistQuote()
        {
            int count_before = quoteDaoEF.GetAll().Count<T>();

            T quote = generateQuote();

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
            quote.User = context.UserSet.First<User>();

            AddQuote(quote);

            int count_after = quoteDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before + 1, count_after);
        }
        
        [TestMethod()]
        public void DeleteShouldDeleteQuote()
        {
            T quote = addedQuotes.Last<T>();

            int count_before = quoteDaoEF.GetAll().Count<T>();

            DeleteQuote(quote);

            int count_after = quoteDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before - 1, count_after);
        }
        
        [TestMethod()]
        public void UpdateShouldSaveChanges()
        {
            T expected = addedQuotes.Last<T>();

            expected.QuoteString = "ChangedTitle";

            quoteDaoEF.Save();

            T actual = quoteDaoEF.GetById(expected.Id);

            Assert.AreEqual(expected.QuoteString, actual.QuoteString);
        }
        
        [TestMethod()]
        public void GetByTitleShouldReturnOneQuote()
        {
            T quote = generateQuote();
            quote.QuoteString = "the best search for quotes";

            quote.User = ContextHelper<MediaLibContainer>
                            .GetCurrentContext().UserSet.First<User>();

            AddQuote(quote);

            IQueryable<T> result = quoteDaoEF.GetByQuoteString("sEarCH");

            Assert.AreEqual(quote.QuoteString, result.Single<T>().QuoteString);
        }
        
        [TestMethod()]
        public void GetByTitleShouldReturnThreeQuotes()
        {
            User user = ContextHelper<MediaLibContainer>
                .GetCurrentContext().UserSet.First<User>();

            T quote1 = generateQuote();
            quote1.QuoteString = "I'll be back!";
            quote1.User = user;
            AddQuote(quote1);

            T quote2 = generateQuote();
            quote2.QuoteString = "I'll be back!";
            quote2.User = user;
            AddQuote(quote2);

            T quote3 = generateQuote();
            quote3.QuoteString = "I'll be back!";
            quote3.User = user;
            AddQuote(quote3);

            IQueryable<T> result = quoteDaoEF.GetByQuoteString("back");

            Assert.AreEqual(3, result.Count<T>());

            foreach (T quote in result)
            {
                Assert.IsTrue(quote.QuoteString.Contains("back"));
            }
        }
        
        [TestMethod()]
        public void GetByTitleShouldNoQuotes()
        {
            IQueryable<T> result = quoteDaoEF.GetByQuoteString("sEarCH");

            Assert.AreEqual(0, result.Count<T>());
        }

        [TestMethod()]
        public void GetByQuoteStringRankedShouldReturnOnlyNonZeroRankedQuotes()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();

            T quote0 = generateQuote();
            quote0.QuoteString = "Ranking 0";
            quote0.Ranking = 0;
            quote0.User = user;
            AddQuote(quote0);

            T quote1 = generateQuote();
            quote1.QuoteString = "Ranking 1";
            quote1.Ranking = 1;
            quote1.User = user;
            AddQuote(quote1);

            T quote2 = generateQuote();
            quote2.QuoteString = "Ranking 2";
            quote2.Ranking = 2;
            quote2.User = user;
            AddQuote(quote2);

            T quote3 = generateQuote();
            quote3.QuoteString = "Ranking 3";
            quote3.Ranking = 3;
            quote3.User = user;
            AddQuote(quote3);

            IQueryable<T> result = quoteDaoEF.GetByQuoteStringRanked("Ranking");

            Assert.AreEqual(3, result.Count<T>());

            foreach (T quote in result)
            {
                Assert.IsTrue(quote.QuoteString.Contains("Ranking"));
            }
        }

        [TestMethod()]
        public void GetByQuoteStringRankedShouldReturnOrderedQuotes()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            User user = context.UserSet.First<User>();

            T quote0 = generateQuote();
            quote0.QuoteString = "Ranking 0";
            quote0.Ranking = 0;
            quote0.User = user;
            AddQuote(quote0);

            T quote1 = generateQuote();
            quote1.QuoteString = "Ranking 1";
            quote1.Ranking = 1;
            quote1.User = user;
            AddQuote(quote1);

            T quote2 = generateQuote();
            quote2.QuoteString = "Ranking 2";
            quote2.Ranking = 2;
            quote2.User = user;
            AddQuote(quote2);

            T quote3 = generateQuote();
            quote3.QuoteString = "Ranking 3";
            quote3.Ranking = 3;
            quote3.User = user;
            AddQuote(quote3);

            IQueryable<T> result = quoteDaoEF.GetByQuoteStringRanked("Ranking");

            Assert.AreEqual(3, result.Count<T>());

            int i = 1;
            foreach (T quote in result)
            {
                Assert.AreEqual(i++, quote.Ranking);
                Assert.IsTrue(quote.QuoteString.Contains("Ranking"));
            }
        }
    }
}
