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
    public class MovieDaoEFTest : AbstractMediaDaoEFTest<Movie>
    {
        protected override Movie generateMedia()
        {
            return TestUtil.generateMovie();
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            mediaDaoEF = MovieDaoEF.Instance;
            TestUtil.ClearDatabase();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }
    }
}