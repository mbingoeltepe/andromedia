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
    public class TV_ShowDaoEFTest : AbstractMediaDaoEFTest<TV_Show>
    {
        protected override TV_Show generateMedia()
        {
            /*
            int i = addedMedia.Count;

            TV_Show tv_show = new TV_Show();
            tv_show.Title = "TV_Show_Title" + i;
            tv_show.OriginalTitle = "TV_Show_OrignialTitle" + i;
            tv_show.ShowBeginning = new DateTime(1999 + i, 1 + i, 1 + i);

            return tv_show;
             * */

            return TestUtil.generateTV_Show(false);
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            mediaDaoEF = TV_ShowDaoEF.Instance;
            TestUtil.ClearDatabase();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }
    }
}