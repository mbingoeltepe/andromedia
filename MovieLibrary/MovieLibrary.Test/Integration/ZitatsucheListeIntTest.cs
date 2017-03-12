using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using MovieLibrary.Daos;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Controllers;
using System.Web;
using System.Web.Hosting;
using System.IO;
using MovieLibrary.Helpers;
using System.Web.Mvc;

namespace MovieLibrary.Test.Integration
{

    /* 
     * Die TestDaten mit der Datei(Test_Inserts.sql) wurden bereit schon in der Datenbank gespeichert.
     * Es werden getestet, ob die Service-Schict(Controller) richtig funktioniert.
     * 
     */

    [TestClass]
    public class ZitatsucheListeIntTest 
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            TestUtil.InsertTestData();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();
        }


       [TestMethod()]
          public void TestViewName()
          {
               var controller = new SearchController();
               var result = controller.ResultAllMedia("") as ViewResult;
              
               Assert.AreEqual("ResultAllMedia", result.ViewName);

          }

      
       /*
        * Alle Zitates eines bestimmten Produkts(Movie) wird gesucht und die Details des Produkts werden vergleicht.
       */

       [TestMethod()]
       public void TestQuotesForMovie()
       {
           

           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('Der Terminator', 'The Terminator', 'Action', 3.44, 'false');
           
           // Produkt(Movie) wird gesucht. Von gefundenen Produkt wird mediaID genommen.

           var controllerSearch = new SearchController();

           var resultSearch = controllerSearch.ResultMovies("Der Terminator") as ViewResult;
           var produktSearch = (List<Movie>)resultSearch.ViewData.Model;
           
           int mediaID = produktSearch.Single<Movie>().Id;


           //insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId)

           // Mit dem mediID wird alle Zitates gesucht. Diese Movie hat vier Zitates.
           var controllerQuote = new QuoteController();

           var resultQuote = controllerQuote.ShowQuotesMedia(mediaID)  as ViewResult;
           var produktQuote = (Movie)resultQuote.ViewData.Model;

           // 1 Quote
           //values ('I''ll be back', 'The Terminator', 'English', 1, 1, 1);

           String gesuchteQuote1 = "I'll be back";

           Assert.AreEqual(gesuchteQuote1, produktQuote.Quote.ElementAt<Quote>(0).QuoteString);
           Assert.AreEqual("The Terminator", produktQuote.Quote.ElementAt<Quote>(0).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(0).Language);


           // 2 Quote
           // values ('Not yet. Not for about 40 years.', 'Kyle Reese', 'English', 2, 1, 1);

           String gesuchteQuote2 = "Not yet. Not for about 40 years.";

           Assert.AreEqual(gesuchteQuote2, produktQuote.Quote.ElementAt<Quote>(1).QuoteString);
           Assert.AreEqual("Kyle Reese", produktQuote.Quote.ElementAt<Quote>(1).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(1).Language);

           // 3 Quote
           // values ('Reese. Why me? Why does it want me?', 'Sarah Connor', 'English', 8, 1, 1);

           String gesuchteQuote3 = "Reese. Why me? Why does it want me?";

           Assert.AreEqual(gesuchteQuote3, produktQuote.Quote.ElementAt<Quote>(2).QuoteString);
           Assert.AreEqual("Sarah Connor", produktQuote.Quote.ElementAt<Quote>(2).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(2).Language);

           // 4 Quote
           // values ('Your clothes... give them to me, now.', 'The Terminator', 'English', 6, 1, 1);

           String gesuchteQuote4 = "Your clothes... give them to me, now.";

           Assert.AreEqual(gesuchteQuote4, produktQuote.Quote.ElementAt<Quote>(3).QuoteString);
           Assert.AreEqual("The Terminator", produktQuote.Quote.ElementAt<Quote>(3).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(3).Language);

          
       }

       /*
        * Alle Zitates eines bestimmten Produkts(Book) wird gesucht und die Details des Produkts werden vergleicht.
       */

       [TestMethod()]
       public void TestQuotesForBook()
       {

           
           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('In einer kleinen Stadt', 'Needful Things', 'Horror', 2.41, 'false');


           // Produkt(Book) wird gesucht. Von gefundenen Produkt wird mediaID genommen.

           var controllerSearch = new SearchController();

           var resultSearch = controllerSearch.ResultBooks("In einer kleinen Stadt") as ViewResult;
           var produktSearch = (List<Book>)resultSearch.ViewData.Model;

           int mediaID = produktSearch.Single<Book>().Id;
           
           
           // Mit dem mediID wird alle Zitates gesucht. Diese Buch hat nur ein Zitat.
           //insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId)

           //Quote
           //values ('It''s aggravations I mostly want to talk about-- can you sit a spell with me?'
           //, 'Old-Timer', 'English', 0, 1, 9);


           var controllerQuote = new QuoteController();

           var resultQuote = controllerQuote.ShowQuotesMedia(mediaID) as ViewResult;
           var produktQuote = (Book)resultQuote.ViewData.Model;


           String gesuchteQuote = "It's aggravations I mostly want to talk about-- can you sit a spell with me?";

           Assert.AreEqual(gesuchteQuote, produktQuote.Quote.Single().QuoteString);
           Assert.AreEqual("Old-Timer", produktQuote.Quote.Single().Character);
           Assert.AreEqual("English", produktQuote.Quote.Single().Language);


       }


        /*
         * Alle Zitates eines bestimmten Produkts(TvShow) wird gesucht und die Details des Produkts werden vergleicht.
        */

       [TestMethod()]
       public void TestQuotesForTvShow()
       {


           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('South Park', 'South Park', 'Comedy', 5, 'false');

           // Produkt(TvShow) wird gesucht. Von gefundenen Produkt wird mediaID genommen.

           var controllerSearch = new SearchController();

           var resultSearch = controllerSearch.ResultTvShows("South Park") as ViewResult;
           var produktSearch = (List<TV_Show>)resultSearch.ViewData.Model;

           int mediaID = produktSearch.Single<TV_Show>().Id;


           //insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId)

           // Mit dem mediID wird alle Zitates gesucht. Diese Movie hat drei Zitates.
           var controllerQuote = new QuoteController();

           var resultQuote = controllerQuote.ShowQuotesMedia(mediaID) as ViewResult;
           var produktQuote = (TV_Show)resultQuote.ViewData.Model;

           // Quote 1
           // values ('TIMAH.', 'Timmy', 'English', 10, 1, 6);

           String gesuchteQuote1 = "TIMAH.";

           Assert.AreEqual(gesuchteQuote1, produktQuote.Quote.ElementAt<Quote>(0).QuoteString);
           Assert.AreEqual("Timmy", produktQuote.Quote.ElementAt<Quote>(0).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(0).Language);

           // Quote 2
           // values ('Drugs are bad, mkay ...', 'Mr. Mackey', 'English', 4, 1, 6);

           String gesuchteQuote2 = "Drugs are bad, mkay ...";

           Assert.AreEqual(gesuchteQuote2, produktQuote.Quote.ElementAt<Quote>(1).QuoteString);
           Assert.AreEqual("Mr. Mackey", produktQuote.Quote.ElementAt<Quote>(1).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(1).Language);

           // Quote 3
           // values ('How do you kill, that which has no life? ', 'Blizzard Employee', 'English', 5, 1, 6);

           String gesuchteQuote3 = "How do you kill, that which has no life? ";

           Assert.AreEqual(gesuchteQuote3, produktQuote.Quote.ElementAt<Quote>(2).QuoteString);
           Assert.AreEqual("Blizzard Employee", produktQuote.Quote.ElementAt<Quote>(2).Character);
           Assert.AreEqual("English", produktQuote.Quote.ElementAt<Quote>(2).Language);


       }
      
    }
}