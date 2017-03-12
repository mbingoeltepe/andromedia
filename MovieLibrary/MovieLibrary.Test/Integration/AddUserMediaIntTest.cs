using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Helpers;
using MovieLibrary.Models;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using System;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class AddUserMediaIntTest
    {
        protected static List<UserMedia> addedUserMedia = new List<UserMedia>();
        protected static Movie movie;
        protected static TV_Show tv_show;
        protected static Book book;
        protected static User user;
        protected static IPrincipal fakeUser;
        protected static IUserMediaService userMediaService = UserMediaService.Instance;

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
                context.MediaSet.AddObject(tvShows);
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
            TestUtil.CreateDummyHttpContext();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteUserMedia();
        }

        [TestMethod]
        public void TestAddBookToUserMedien()
        {
            AddBooks(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Book book = context.MediaSet.OfType<Book>().First<Book>();
            int expectedBookId = book.Id;

            MediathekController mediathekController = new MediathekController();

            
            FormCollection collection = new FormCollection();

            collection["Status"] =  UserMediaStatusEnum.NichtVerborgt.ToString();
            collection["Storage"] = MediaAufberwahrungsortEnum.Wohnzimmer.ToString();

            ViewResult result = mediathekController.AddBook(book.Id, collection, fakeUser) as ViewResult;
            
            IQueryable<UserBook> userBooks = UserMediaService.Instance.GetUserBookByIdAndUserName(fakeUser.Identity.Name, book.Id);

            addedUserMedia.Add(userBooks.Single<UserBook>());

            Assert.IsNotNull(userBooks);
            Assert.AreEqual(expectedBookId, userBooks.Single<UserBook>().Book.Id);
            Assert.AreEqual(collection["Status"], userBooks.Single<UserBook>().MediaStatus);
            Assert.AreEqual(collection["Storage"], userBooks.Single<UserBook>().StoragePlace);

        }

  
        [TestMethod]
        public void TestAddMovieToUserMedien()
        {
            AddMovies(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Movie movie = context.MediaSet.OfType<Movie>().First<Movie>();
            int expectedVideoId = movie.Id;

            MediathekController mediathekController = new MediathekController();


            FormCollection collection = new FormCollection();

            collection["Status"] = UserMediaStatusEnum.NichtVerborgt.ToString();
            collection["Storage"] = MediaAufberwahrungsortEnum.Wohnzimmer.ToString();
            collection["Device"] = UserMediaStorageDeviceEnum.BluRay.ToString();

            
            ViewResult result = mediathekController.AddMovie(movie.Id, collection, fakeUser) as ViewResult;

            IQueryable<UserMovie> userVideos = UserMediaService.Instance.GetUserMovieByIdAndUserName(fakeUser.Identity.Name, movie.Id);

            addedUserMedia.Add(userVideos.Single<UserMovie>());

            Assert.IsNotNull(userVideos);
            Assert.AreEqual(expectedVideoId, userVideos.Single<UserMovie>().MovieId);
            Assert.AreEqual(collection["Status"], userVideos.Single<UserMovie>().MediaStatus);
            Assert.AreEqual(collection["Storage"], userVideos.Single<UserMovie>().StoragePlace);
            Assert.AreEqual(collection["Device"], userVideos.Single<UserMovie>().StorageType);


        }

        [TestMethod]
        public void TestAddTvShowToUserMedien()
        {
            AddTvShows(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            TV_Show tv_show = context.MediaSet.OfType<TV_Show>().First<TV_Show>();
            int expectedVideoId = tv_show.Id;

            MediathekController mediathekController = new MediathekController();


            FormCollection collection = new FormCollection();


            collection["Season"] = "1";
            collection["Status"] = UserMediaStatusEnum.Verborgt.ToString();
            collection["Storage"] = MediaAufberwahrungsortEnum.Keller.ToString();
            collection["Device"] = UserMediaStorageDeviceEnum.Dvd.ToString();

            ViewResult result = mediathekController.AddTvShow(collection, fakeUser) as ViewResult;

            IQueryable<UserTV_Show> userVideos = UserMediaService.Instance.GetUserTvShowByIdAndUserName(fakeUser.Identity.Name, tv_show.Id);

            addedUserMedia.Add(userVideos.Single<UserTV_Show>());

            Assert.IsNotNull(userVideos);
            Assert.AreEqual(expectedVideoId, userVideos.Single<UserTV_Show>().Season.TV_ShowId);
            Assert.AreEqual(Convert.ToInt32(collection["Season"]), userVideos.Single<UserTV_Show>().Season.Number);
            Assert.AreEqual(collection["Storage"], userVideos.Single<UserTV_Show>().StoragePlace);
            Assert.AreEqual(collection["Device"], userVideos.Single<UserTV_Show>().StorageType);

        }
         


    }
}
