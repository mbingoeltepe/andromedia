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
    public class BookDaoEFTest : AbstractMediaDaoEFTest<Book>
    {
        protected override Book generateMedia()
        {
            return TestUtil.generateBook();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            mediaDaoEF = BookDaoEF.Instance;
            TestUtil.ClearDatabase();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }
    }
}