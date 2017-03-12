using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public class QuoteBookDaoEFTest : AbstractQuoteDaoEFTest<QuoteBook>
    {
        protected override QuoteBook generateQuote()
        {
            return TestUtil.generateQuoteBook();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestUser();
            quoteDaoEF = QuoteBookDaoEF.Instance;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }
    }
}
