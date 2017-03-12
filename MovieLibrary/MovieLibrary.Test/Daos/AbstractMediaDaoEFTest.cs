using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Daos;
using MovieLibrary.Daos.EntityFramework;
using System.Web;
using System.Web.Hosting;
using System.IO;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public abstract class AbstractMediaDaoEFTest<T> where T : Media
    {
        protected static List<T> addedMedia = new List<T>();
        protected static AbstractMediaDaoEF<T> mediaDaoEF;
        protected const int COUNT = 5;

        protected abstract T generateMedia();

        protected void AddMedia(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
                for (int i = 1; i <= count; i++)
                {
                    T media = generateMedia();
                    context.MediaSet.AddObject(media);
                    addedMedia.Add(media);
                }
                context.SaveChanges();
        }

        protected void DeleteMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
                foreach (T media in addedMedia)
                {
                    context.MediaSet.DeleteObject(media);
                }
                context.SaveChanges();
            
            addedMedia.Clear();
        }

        protected void AddMedia(T media)
        {
            mediaDaoEF.Add(media);
            addedMedia.Add(media);
        }

        protected void DeleteMedia(T media)
        {
            mediaDaoEF.Delete(media);
            addedMedia.Remove(media);
        }

        protected void CreateDummyHttpContext()
        {
            HttpWorkerRequest request = new SimpleWorkerRequest("/dummy", @"c:\inetpub\wwwroot\dummy", "dummy.html", null, new StringWriter());
            HttpContext.Current = new HttpContext(request);
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            CreateDummyHttpContext();

            AddMedia(COUNT);
        }
        
        [TestCleanup()]
        public void TestCleanup()
        {
            DeleteMedia();
        }

        [TestMethod()]
        public void GetAllShouldReturnFiveMedia()
        {
            IQueryable<T> media = mediaDaoEF.GetAll();
            Assert.AreEqual(addedMedia.Count, media.Count<T>());
        }

        
        [TestMethod()]
        public void GetAllShouldReturnNoMedia()
        {
            DeleteMedia();
            IQueryable<T> media = mediaDaoEF.GetAll();
            Assert.AreEqual(0, media.Count<T>());
        }
        
        [TestMethod()]
        public void GetByIdShouldReturnMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            T media_expected = context.MediaSet.OfType<T>().First<T>();
            T media = mediaDaoEF.GetById(media_expected.Id);

            Assert.AreEqual(media.Id, media_expected.Id);
        }
        
        [TestMethod()]
        public void GetByIdShouldReturnNull()
        {
            Assert.IsNull(mediaDaoEF.GetById(-1));
        }
        
        [TestMethod()]
        public void AddShouldPersistMedia()
        {
            T media = generateMedia();

            int count_before = mediaDaoEF.GetAll().Count<T>();

            AddMedia(media);

            int count_after = mediaDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before + 1, count_after);
        }

        [TestMethod()]
        public void DeleteShouldDeleteMedia()
        {
            T media = addedMedia.Last<T>();

            int count_before = mediaDaoEF.GetAll().Count<T>();

            DeleteMedia(media);

            int count_after = mediaDaoEF.GetAll().Count<T>();

            Assert.AreEqual(count_before - 1, count_after);
        }
        
        [TestMethod()]
        public void UpdateShouldSaveChanges()
        {
            T expected = addedMedia.Last<T>();

            expected.Title = "ChangedTitle";

            mediaDaoEF.Save();

            T actual = mediaDaoEF.GetById(expected.Id);

            Assert.AreEqual(expected.Title, actual.Title);
        }
        
        [TestMethod()]
        public void GetByTitleShouldReturnOneMedia()
        {
            T media = generateMedia();
            media.Title = "the best search for media";

            AddMedia(media);

            IQueryable<T> result = mediaDaoEF.GetByTitle("sEarCH");

            Assert.AreEqual(media.Title, result.Single<T>().Title);
        }
        
        [TestMethod()]
        public void GetByTitleShouldReturnThreeMedia()
        {
            T media1 = generateMedia();
            media1.Title = "Scary Movie";
            AddMedia(media1);

            T media2 = generateMedia();
            media2.Title = "Scary Movie";
            AddMedia(media2);

            T media3 = generateMedia();
            media3.Title = "Scary Movie";
            AddMedia(media3);

            IQueryable<T> result = mediaDaoEF.GetByTitle("Scary");

            Assert.AreEqual(3, result.Count<T>());

            foreach (T media in result)
            {
                Assert.IsTrue(media.Title.Contains("Scary"));
            }
        }

        [TestMethod()]
        public void GetByTitleShouldNoMedia()
        {
            IQueryable<T> result = mediaDaoEF.GetByTitle("sEarCH");

            Assert.AreEqual(0, result.Count<T>());
        }
    }
}