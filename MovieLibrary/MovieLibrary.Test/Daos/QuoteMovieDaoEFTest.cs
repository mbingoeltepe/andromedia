using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Test.Daos;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;

namespace MovieLibrary.Test.Dao
{
    [TestClass]
    public class QuoteMovieDaoEFTest : AbstractQuoteDaoEFTest<QuoteMovie>
    {
        protected override QuoteMovie generateQuote()
        {
            return TestUtil.generateQuoteMovie();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
            quoteDaoEF = QuoteMovieDaoEF.Instance;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }
    }
}
