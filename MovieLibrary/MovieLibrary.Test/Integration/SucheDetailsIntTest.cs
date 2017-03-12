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
    public class SucheDetailsIntTest 
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
        * Ein bestimmtes Produkt(Movie) wird gesucht und die Details des Produkts werden vergleicht.
        */
       [TestMethod()]
       public void TestViewDataForMovie()
       {
                
           var controller = new SearchController();

           var result = controller.ResultMovies("Der Terminator") as ViewResult;
           var produkt = (List<Movie>) result.ViewData.Model;

           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('Der Terminator', 'The Terminator', 'Action', 3.44, 'false');
          
           Assert.AreEqual("Der Terminator", produkt.Single<Movie>().Title);
           Assert.AreEqual("The Terminator", produkt.Single<Movie>().OriginalTitle);
           Assert.AreEqual("Action", produkt.Single<Movie>().Genre);
           Assert.AreEqual(4, produkt.Single<Movie>().Rating);
           Assert.AreEqual(false, produkt.Single<Movie>().Pending);
       }

       /*
        * Ein bestimmtes Produkt(Book) wird gesucht und die Details des Produkts werden vergleicht.
        */

       [TestMethod()]
       public void TestViewDataForBook()
       {

           var controller = new SearchController();

           var result = controller.ResultBooks("In einer kleinen Stadt") as ViewResult;
           var produkt = (List<Book>)result.ViewData.Model;


           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('In einer kleinen Stadt', 'Needful Things', 'Horror', 2.41, 'false');

           Assert.AreEqual("In einer kleinen Stadt", produkt.Single<Book>().Title);
           Assert.AreEqual("Needful Things", produkt.Single<Book>().OriginalTitle);
           Assert.AreEqual("Horror", produkt.Single<Book>().Genre);
           Assert.AreEqual(2, produkt.Single<Book>().Rating);
           Assert.AreEqual(false, produkt.Single<Book>().Pending);
       }

       /*
        * Ein bestimmtes Produkt(TV-Show) wird gesucht und die Details des Produkts werden vergleicht.
       */

       [TestMethod()]
       public void TestViewDataForTvShow()
       {

           var controller = new SearchController();

           var result = controller.ResultTvShows("South Park") as ViewResult;
           var produkt = (List<TV_Show>)result.ViewData.Model;


           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('South Park', 'South Park', 'Comedy', 5, 'false');

           Assert.AreEqual("South Park", produkt.Single<TV_Show>().Title);
           Assert.AreEqual("South Park", produkt.Single<TV_Show>().OriginalTitle);
           Assert.AreEqual("Comedy", produkt.Single<TV_Show>().Genre);
           Assert.AreEqual(10,produkt.Single<TV_Show>().Rating);
           Assert.AreEqual(false, produkt.Single<TV_Show>().Pending);
       }

      
    }
}