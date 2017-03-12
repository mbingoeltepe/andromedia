using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using System.Security.Principal;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class UserProfilansichtIntTest
    {
        protected static List<UserMedia> addedUserMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_Show;
        protected static Book book;
        protected static User user;
        protected static IPrincipal fakeUser;
        protected static IUserMediaService userMediaService = UserMediaService.Instance;

        protected void AddUserBooks(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserBook userBook = TestUtil.generateUserBook();
                userBook.User = context.UserSet.First<User>();
                userBook.Book = context.MediaSet.OfType<Book>().First<Book>();
                context.UserMediaSet.AddObject(userBook);
                addedUserMedia.Add(userBook);
            }
            context.SaveChanges();
        }

        protected void AddUserMovies(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserMovie userMovie = TestUtil.generateUserMovie();
                userMovie.User = context.UserSet.First<User>();
                userMovie.Movie = context.MediaSet.OfType<Movie>().First<Movie>();
                context.UserMediaSet.AddObject(userMovie);
                addedUserMedia.Add(userMovie);
            }
            context.SaveChanges();
        }

        protected void AddUserTV_Shows(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                UserTV_Show userTV_Show = TestUtil.generateUserTV_Show();
                userTV_Show.User = context.UserSet.First<User>();
                userTV_Show.Season.TV_Show = context.MediaSet.OfType<TV_Show>().First<TV_Show>();
                context.UserMediaSet.AddObject(userTV_Show);
                addedUserMedia.Add(userTV_Show);
            }
            context.SaveChanges();
        }

        protected void DeleteUserMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (UserMedia userMedia in addedUserMedia)
            {
                context.UserMediaSet.DeleteObject(userMedia);
            }
            context.SaveChanges();

            addedUserMedia.Clear();
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
                tv_Show = TestUtil.generateTV_Show(false);

                context.UserSet.AddObject(user);
                context.MediaSet.AddObject(book);
                context.MediaSet.AddObject(movie);
                context.MediaSet.AddObject(tv_Show);
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
            TestUtil.CreateDummyHttpContext();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteUserMedia();
        }

        [TestMethod()]
        public void TestViewName()
        {
            var controller = new UserController();
            var result = controller.ShowProfile(fakeUser) as ViewResult;

            Assert.AreEqual("ShowProfile", result.ViewName);

        }

        [TestMethod()]
        public void TestUserBooksViewData()
        {
            AddUserBooks(1);
            
            var controller = new UserController();

            var result = controller.ShowProfile(fakeUser) as ViewResult;
            var produkt = (User)result.ViewData.Model;


            Assert.IsNotNull(produkt);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).Id, produkt.UserMedien.Single().Id);

            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).MediaStatus, produkt.UserMedien.Single().MediaStatus);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).StoragePlace, produkt.UserMedien.Single().StoragePlace);

            
        }

        [TestMethod()]
        public void TestUserMoviesViewData()
        {
            AddUserMovies(1);

            var controller = new UserController();

            var result = controller.ShowProfile(fakeUser) as ViewResult;
            var produkt = (User)result.ViewData.Model;

            Assert.IsNotNull(produkt);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).Id, produkt.UserMedien.Single().Id);

            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).MediaStatus, produkt.UserMedien.Single().MediaStatus);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).StoragePlace, produkt.UserMedien.Single().StoragePlace);


        }

        [TestMethod()]
        public void TestUserTvShowsViewData()
        {
            AddUserTV_Shows(1);

            var controller = new UserController();

            var result = controller.ShowProfile(fakeUser) as ViewResult;
            var produkt = (User)result.ViewData.Model;

            Assert.IsNotNull(produkt);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).Id, produkt.UserMedien.Single().Id);

            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).MediaStatus, produkt.UserMedien.Single().MediaStatus);
            Assert.AreEqual(addedUserMedia.ElementAt<UserMedia>(0).StoragePlace, produkt.UserMedien.Single().StoragePlace);


        }

         
    }
}
