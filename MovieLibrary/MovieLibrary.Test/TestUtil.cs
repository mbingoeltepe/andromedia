using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Web;
using System.Web.Hosting;
using MovieLibrary.Models;
using System.Xml;
using System.Web.Mvc;
using Rhino.Mocks;

namespace MovieLibrary.Test
{
    public sealed class TestUtil
    {
        private static string GetSolutionPath()
        {
            string current_path = AppDomain.CurrentDomain.BaseDirectory;
            return current_path.Remove(current_path.LastIndexOf(@"\TestResults\"));
        }

        private static void ExecuteTSQLScript(string relPath)
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["MediaLibConnectionString"];
            string strConnString = conString.ConnectionString;

            //string current_path = AppDomain.CurrentDomain.BaseDirectory;
            //string path = current_path.Remove(current_path.LastIndexOf(@"\TestResults\"));
            string path = GetSolutionPath();
            path += relPath;

            FileInfo file = new FileInfo(path);
            string script = file.OpenText().ReadToEnd();
            SqlConnection con = new SqlConnection(strConnString);
            Server server = new Server(new ServerConnection(con));

            con.Open();
            server.ConnectionContext.ExecuteNonQuery(script);
            con.Close();
        }

        public static void ClearDatabase()
        {
            ExecuteTSQLScript(@"\MovieLibrary\Models\MediaLib.edmx.sql");
        }

        public static void InsertTestData()
        {
            ExecuteTSQLScript(@"\MovieLibrary.Test\TestData\Test_Inserts.sql");
        }

        public static void CreateDummyHttpContext()
        {
            HttpWorkerRequest request = new SimpleWorkerRequest("/dummy", @"c:\inetpub\wwwroot\dummy", "dummy.html", null, new StringWriter());
            HttpContext.Current = new HttpContext(request);
        }

        public static void CreateDummyHttpContextWithCookie()
        {
 
            HttpWorkerRequest request = new SimpleWorkerRequest("/dummy", @"c:\inetpub\wwwroot\dummy", "dummy.html", null, new StringWriter());

            HttpContext.Current = new HttpContext(request);
           

            HttpCookie voteCookie = new HttpCookie("Votes");


            HttpContext.Current.Response.Cookies.Add(voteCookie);    
             

        }


        public static XmlDocument GetXmlDocFromFile(string relPath)
        {
            string path = GetSolutionPath();
            XmlTextReader reader = new XmlTextReader(path + relPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return doc;
        }

        public static Book generateBook()
        {
            Book book = new Book();
            book.Title = "TV_Show_Title";
            book.OriginalTitle = "TV_Show_OrignialTitle";
            book.Rating = 5.0;
            book.TotalRaters = 2;
            book.AverageRating = 2.50;
            book.AddingDate = DateTime.Now;

            return book;
        }

        public static Movie generateMovie()
        {
            Movie movie = new Movie();
            movie.Title = "Movie_Title";
            movie.OriginalTitle = "Movie_OriginalTitle";
            movie.ReleaseDate = new DateTime(1999, 1, 1);
            movie.AddingDate = DateTime.Now;

            return movie;
        }

        public static TV_Show generateTV_Show(bool withEpisode)
        {
            TV_Show tv_show = new TV_Show();
            tv_show.Title = "TV_Show_Title";
            tv_show.OriginalTitle = "TV_Show_OrignialTitle";
            tv_show.ShowBeginning = new DateTime(1999, 1, 1);
            tv_show.AddingDate = DateTime.Now;

            if (withEpisode)
            {
                Season season = new Season();
                season.Number = 1;
                season.TV_Show = tv_show;

                Episode episode = new Episode();
                episode.Name = "testepisode";
                episode.Season = season;
            }

            return tv_show;
        }

        public static QuoteBook generateQuoteBook()
        {
            QuoteBook quoteBook = new QuoteBook();
            quoteBook.QuoteString = "quoteString";
            quoteBook.Language = "English";
            quoteBook.Character = "T-1000";
            quoteBook.Ranking = 1;

            Book book = generateBook();

            quoteBook.Book = book;
            quoteBook.Media = book;

            return quoteBook;
        }

        public static QuoteMovie generateQuoteMovie()
        {
            QuoteMovie quoteMovie = new QuoteMovie();
            quoteMovie.QuoteString = "quoteString";
            quoteMovie.Language = "English";
            quoteMovie.Character = "T-1000";
            quoteMovie.Ranking = 1;

            Movie movie = generateMovie();

            quoteMovie.Movie = movie;
            quoteMovie.Media = movie;

            return quoteMovie;
        }

        public static QuoteTV_Show generateQuoteTV_Show()
        {
            QuoteTV_Show quoteTV_Show = new QuoteTV_Show();
            quoteTV_Show.QuoteString = "quoteString";
            quoteTV_Show.Language = "English";
            quoteTV_Show.Character = "T-1000";
            quoteTV_Show.Ranking = 1;

            TV_Show tv_show = generateTV_Show(true);
            Episode episode = tv_show.Season.First<Season>().Episode.First<Episode>();

            quoteTV_Show.Episode = episode;
            quoteTV_Show.Media = tv_show;

            return quoteTV_Show;
        }

        public static UserBook generateUserBook()
        {
            UserBook userBook = new UserBook();
            userBook.Book = generateBook();
            userBook.StoragePlace = "Dachboden";
            userBook.MediaStatus = "NichtVerborgt";

            return userBook;
        }

        public static UserBook generateUserBook(Book book)
        {
            UserBook userBook = new UserBook();
            userBook.Book = book;
            userBook.StoragePlace = "Dachboden";
            userBook.MediaStatus = "NichtVerborgt";

            return userBook;
        }

        public static UserMovie generateUserMovie()
        {
            UserMovie userMovie = new UserMovie();
            userMovie.Movie = generateMovie();
            userMovie.StorageType = "Dvd";
            userMovie.StoragePlace = "Dachboden";
            userMovie.MediaStatus = "NichtVerborgt";

            return userMovie;
        }

        public static UserMovie generateUserMovie(Movie movie)
        {
            UserMovie userMovie = new UserMovie();
            userMovie.Movie = movie;
            userMovie.StorageType = "Dvd";
            userMovie.StoragePlace = "Dachboden";
            userMovie.MediaStatus = "NichtVerborgt";

            return userMovie;
        }

        public static UserTV_Show generateUserTV_Show()
        {
            UserTV_Show userTV_Show = new UserTV_Show();
            userTV_Show.Season = new Season { Number = 1, Id = 666, TV_Show = generateTV_Show(false) };
            userTV_Show.StorageType = "BlueRay";
            userTV_Show.StoragePlace = "Arbeitszimmer";
            userTV_Show.MediaStatus = "NichtVerborgt";

            return userTV_Show;
        }

        public static UserTV_Show generateUserTV_Show(Season season)
        {
            UserTV_Show userTV_Show = new UserTV_Show();
            userTV_Show.Season = season;
            userTV_Show.StorageType = "BlueRay";
            userTV_Show.StoragePlace = "Arbeitszimmer";
            userTV_Show.MediaStatus = "NichtVerborgt";

            return userTV_Show;
        }

        public static InsertRequest generateInsertRequest()
        {
            Random random = new Random();

            Media media;
            int next = random.Next(3);

            switch (next)
            {
                case 0: media = TestUtil.generateBook();
                    break;
                case 1: media = TestUtil.generateMovie();
                    break;
                case 2: media = TestUtil.generateTV_Show(false);
                    break;
                default: throw new ArgumentException();
            }

            InsertRequest insertRequest = new InsertRequest();
            insertRequest.Media = media;
            insertRequest.RequestDate = DateTime.Now;

            return insertRequest;
        }

        public static MovieLibrary.Models.User CreateUser()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                MovieLibrary.Models.User user = new MovieLibrary.Models.User();
                user.Username = "test@email.com";
                user.Password = "pwd";

                context.UserSet.AddObject(user);
                context.SaveChanges();

                return user;
            }
        }

        public static MovieLibrary.Models.Admin CreateAdmin()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                MovieLibrary.Models.Admin admin = new MovieLibrary.Models.Admin();
                admin.Username = "admin@andromedia.com";
                admin.Password = "admin";

                context.UserSet.AddObject(admin);
                context.SaveChanges();

                return admin;
            }
        }
    }
}