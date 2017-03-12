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
    public class SucheListeIntTest 
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
        * Ein bestimmtes Produkt wird gesucht.
        */
       [TestMethod()]
       public void TestViewData()
       {
                
           var controller = new SearchController();

           var result = controller.ResultMovies("Shining") as ViewResult;
           var produkt = (List<Movie>) result.ViewData.Model;
            
           //insert into MediaLib.dbo.MediaSet (Title, OriginalTitle, Genre, Rating, Pending)
           //values ('Shining', 'The Shining', 'Horror', 4.12, 'false');
                      
           Assert.AreEqual("Shining", produkt.Single<Movie>().Title);
           
      

       }

      
    }
}