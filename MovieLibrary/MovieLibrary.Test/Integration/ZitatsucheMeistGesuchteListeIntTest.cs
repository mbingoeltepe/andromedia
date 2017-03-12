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
using System.Collections;

namespace MovieLibrary.Test.Integration
{

    /* 
     * Die TestDaten mit der Datei(Test_Inserts.sql) wurden bereit schon in der Datenbank gespeichert.
     * Es werden getestet, ob die Service-Schict(Controller) richtig funktioniert.
     * 
     */

    [TestClass]
    public class ZitatsucheMeistGesuchteListeIntTest 
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
        * Meist gesuchte Zitates werden gesucht und die Details des Produkts werden vergleicht.
       */

       [TestMethod()]
       public void TestTopQuotes()
       {
           
           var controllerQuote = new QuoteController();

           var resultQuote = controllerQuote.TopQuotes(0)  as ViewResult;
           var produktQuote = (List<TopQuotesViewModel>)resultQuote.ViewData.Model;


           // Folgende Quotes sind bereits in der Datenbank gespeichert. Die Reihenfolge:

           // insert into MediaLib.dbo.QuoteSet (QuoteString, Character, Language, Invocations, UserId, MediaId)
           // values ('TIMAH.', 'Timmy', 'English', 10, 1, 6);
           // values ('Honey! C''mon over here, Sugar-buns. This machine just called me an asshole!', 'Man At Cashpoint', 'English', 9, 1, 4);
           // values ('Reese. Why me? Why does it want me?', 'Sarah Connor', 'English', 8, 1, 1);
           // values ('You son of a bitch!', 'Lily Aldrin', 'English', 7, 1, 8);
           // values ('Moist', 'Barney Stinson', 'English', 6, 1, 8);
           // values ('Your clothes... give them to me, now.', 'The Terminator', 'English', 6, 1, 1);
           // values ('I don''t know half of you half as well as I should like; and I like less than half of you half as well as you deserve.', 'Bilbo Baggins', 'English', 6, 1, 10);
           // values ('Heeere''s Johnny!', 'Jack Torrance', 'English', 5, 1, 5 );
           // values ('How do you kill, that which has no life? ', 'Blizzard Employee', 'English', 5, 1, 6);
           // values ('Drugs are bad, mkay ...', 'Mr. Mackey', 'English', 4, 1, 6);

           ArrayList gesuchteTopQuotes = new ArrayList();

           gesuchteTopQuotes.Add("TIMAH.");
           gesuchteTopQuotes.Add("Honey! C'mon over here, Sugar-buns. This machine just called me an asshole!");
           gesuchteTopQuotes.Add("Reese. Why me? Why does it want me?");
           gesuchteTopQuotes.Add("You son of a bitch!");
           gesuchteTopQuotes.Add("I don't know half of you half as well as I should like; and I like less than half of you half as well as you deserve.");

           for (int i = 0; i <= gesuchteTopQuotes.Count - 1; i++)
           {
               Assert.AreEqual(gesuchteTopQuotes[i], produktQuote.ElementAt<TopQuotesViewModel>(i).Quote.QuoteString);

           }

           Assert.AreEqual(10, produktQuote.Count);


       }

      
    }
}