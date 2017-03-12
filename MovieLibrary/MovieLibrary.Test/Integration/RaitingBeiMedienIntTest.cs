using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Helpers;
using MovieLibrary.Models;
using MovieLibrary.Service.ServicesImpl;
using System.IO;
using System.Web.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace MovieLibrary.Test.Integration
{

    [TestClass]
    public class RaitingBeiMedienIntTest
    {
        protected static List<UserMedia> addedMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_show;
        protected static Book book;
        protected static User user;
        protected static IPrincipal fakeUser;
        

        protected void AddBooks(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                Book book = TestUtil.generateBook();
                context.MediaSet.AddObject(book);
            }
            context.SaveChanges();
        }

        protected void AddMovies(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                Movie movie = TestUtil.generateMovie();
                movie.Rating = 10;
                movie.TotalRaters = 5;
                movie.AverageRating = 2;

                context.MediaSet.AddObject(movie);
            }
            context.SaveChanges();
        }

        protected void AddTvShows(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                TV_Show tvShows = TestUtil.generateTV_Show(true);
                tvShows.Rating = 3;
                tvShows.TotalRaters = 1;
                tvShows.AverageRating = 1;
                context.MediaSet.AddObject(tvShows);
            }
            context.SaveChanges();
        }

        protected void DeleteMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

  
            foreach (UserMedia media in addedMedia)
            {
                context.UserMediaSet.DeleteObject(media);
            }
            context.SaveChanges();

            addedMedia.Clear();
        }

        protected static void CreateTestContext()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                user = new User();
                user.Username = "test@test.com";
                user.Password = "pwd";

                book = TestUtil.generateBook();
                movie = TestUtil.generateMovie();
                tv_show = TestUtil.generateTV_Show(true);

                context.UserSet.AddObject(user);
                context.MediaSet.AddObject(book);
                context.MediaSet.AddObject(movie);
                context.MediaSet.AddObject(tv_show);
                context.SaveChanges();
            }

            fakeUser = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null);
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            CreateTestContext();

        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContextWithCookie();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteMedia();
        }

        [TestMethod()]
        public void TestViewName()
        {
            var controller = new MediaController();
            var result = controller.TopRated() as ViewResult;

            Assert.AreEqual("../Media/TopRatedMedia", result.ViewName);

        }

        //[TestMethod]
        //public void TestRaitingBook()
        //{
        //    AddBooks(1);

        //    MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
        //    Book book = context.MediaSet.OfType<Book>().First<Book>();
        //    int expectedBookId = book.Id;

        //    double raiting = 4;
            
        //    MediaController mediaController = new MediaController(); 

        //    ViewResult result = mediaController.Rating(expectedBookId, raiting) as ViewResult;

        //    Book books = MediaService.Instance.GetBookById(book.Id);

            
        //    Assert.IsNotNull(books);

        //    Assert.AreEqual(9, books.Rating);
        //    Assert.AreEqual(3, books.TotalRaters);
        //    Assert.AreEqual(3, books.AverageRating);


        //}

  

    }
}
