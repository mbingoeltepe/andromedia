using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using System.Security.Principal;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using MovieLibrary.Service.ServicesImpl;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class AddMediaIntTest
    {
        protected static List<Media> addedMedia = new List<Media>();
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


            foreach (Media media in addedMedia)
            {
                if (media.InsertRequest != null)
                    context.InsertRequestSet.DeleteObject(media.InsertRequest);
                if (media.GetType() == typeof(TV_Show))
                {
                    List<Season> seasons = new List<Season>();
                    List<Episode> episodes = new List<Episode>();
                    foreach (var season in (media as TV_Show).Season)
                    {
                        foreach (var episode in season.Episode)
                        {
                            episodes.Add(episode);
                        }
                        seasons.Add(season);
                    }
                    foreach (var e in episodes)
                    {
                        context.EpisodeSet.DeleteObject(e);
                    }
                    foreach (var s in seasons)
                    {
                        context.SeasonSet.DeleteObject(s);
                    }
                }
                context.MediaSet.DeleteObject(media);
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

        [TestMethod]
        public void TestAddBookToMedia()
        {
            AddBooks(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Book book = context.MediaSet.OfType<Book>().First<Book>();
            int expectedBookId = book.Id;

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();

            collection["ctl00$MainContent$BookTitle"] = book.Title;
            collection["ctl00$MainContent$BookTitleOriginal"] = book.OriginalTitle;
            collection["GenreList"] = book.Genre;
            collection["ctl00$MainContent$Isbn"] = book.Isbn;

            ViewResult result = mediaController.AddBookToDb(collection, fakeUser) as ViewResult;
            Book b = MediaService.Instance.GetBookById(book.Id);

            addedMedia.Add(b);

            Assert.IsNotNull(b);
            Assert.AreEqual(expectedBookId, b.Id);
        }

        [TestMethod]
        public void TestAddMovieToMedia()
        {
            AddMovies(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Movie movie = context.MediaSet.OfType<Movie>().First<Movie>();
            int expectedMovieId = movie.Id;

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();

            collection["ctl00$MainContent$MovieTitle"] = movie.Title;
            collection["ctl00$MainContent$MovieTitleOriginal"] = movie.OriginalTitle;
            collection["GenreList"] = movie.Genre;
            collection["ctl00$MainContent$Datepicker"] = movie.ReleaseDate.ToShortDateString();

            ViewResult result = mediaController.AddMovieToDb(collection, fakeUser) as ViewResult;
            Movie m = MediaService.Instance.GetMovieById(movie.Id);

            addedMedia.Add(m);

            Assert.IsNotNull(m);
            Assert.AreEqual(expectedMovieId, m.Id);
        }

        [TestMethod]
        public void TestAddTvShowToMedia()
        {
            AddTvShows(1);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            TV_Show show = context.MediaSet.OfType<TV_Show>().First<TV_Show>();
            int expectedShowId = show.Id;

            MediaController mediaController = new MediaController();

            FormCollection collection = new FormCollection();

            collection["ctl00$MainContent$TvShowTitle"] = show.Title;
            collection["ctl00$MainContent$TvShowTitleOriginal"] = show.OriginalTitle;
            collection["GenreList"] = show.Genre;
            collection["ctl00$MainContent$DatepickerBeginning"] = show.ShowBeginning.ToShortDateString();
            if(show.ShowEnding != null)
                collection["ctl00$MainContent$DatepickerEnding"] = show.ShowEnding.Value.ToShortDateString();
            collection["SeasonList"] = show.Season.Count.ToString();

            ViewResult result = mediaController.AddTvShowToDb(collection, fakeUser) as ViewResult;
            TV_Show s = MediaService.Instance.GetTvShowById(show.Id);

            addedMedia.Add(s);

            Assert.IsNotNull(s);
            Assert.AreEqual(expectedShowId, s.Id);
        }
    }
}
