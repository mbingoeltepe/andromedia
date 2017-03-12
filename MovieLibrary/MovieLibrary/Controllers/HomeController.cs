using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Helpers;

namespace MovieLibrary.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        //[RequireHttps]
        public ActionResult Index()
        {
            
            //var media = new List<Media>();
            //foreach (var movie in MovieDaoEF.Instance.GetAll())
            //{
            //    media.Add(movie);
            //}
            
            //var movies = movieDao.GetAllMovies();
            //var books = bookDao.getAllBooks().ToList();
            
            return View(MediaService.Instance.GetLast10AddedTitles());
        }

       
        //public void AddMovie()
        //{
        //    /*------ TEST QUOTE ------*/
        //    //QuoteMovie qm = new QuoteMovie();
        //    //qm.QuoteString = "I don't think she sees me";
        //    //qm.Language = "Englisch";
        //    //qm.Movie.Add(MovieDaoEF.Instance.GetById(1));
            
        //    //QuoteRanking rank = new QuoteRanking();
        //    //rank.MediaId = MovieDaoEF.Instance.GetById(1).Id;
        //    //rank.QuoteId = 


        //    /*------ TEST TV-SHOW ------*/
        //    TV_Show show = new TV_Show();
        //    show.Title = "Eine Himmlische Familie";
        //    show.OriginalTitle = "Seventh Heaven";
        //    show.Genre = "Familie";
        //    show.Cover = null;
        //    show.ShowBeginning = new DateTime(1998, 12, 12);
        //    show.ShowEnding = new DateTime(2004, 1, 30);
        //    show.Rating = 2.5;
        //    TV_ShowDaoEF.Instance.Add(show);


        //    /*------ TEST BUCH ------*/
        //    Book b = new Book();

        //    b.Title = "The Book of Seven Truths";
        //    b.OriginalTitle = b.Title;
        //    b.Isbn = "978-0060657536";
        //    b.Genre = "Drama";
        //    b.Cover = null;

        //    Author a = new Author();
        //    a.FirstName = "Calvin";
        //    a.LastName = "Miller";

        //    b.Author.Add(a);

        //    BookDaoEF.Instance.Add(b);


        //    /*------ TEST MOVIE inkl BUCH ------*/
        //    Movie mov = new Movie();
        //    mov.Genre = "Drama";
        //    mov.OriginalTitle = "Seven Pounds";
        //    mov.Title = "Sieben Leben";
        //    mov.Rating = 4.0;
        //    mov.Pending = false;
        //    mov.Cover = null;
        //    mov.ReleaseDate = new DateTime(2009, 12, 14);

        //    Book book = new Book();

        //    Author author = new Author();
        //    author.FirstName = "Stefan";
        //    author.LastName = "Vöber";

        //    book.Author.Add(author);
        //    book.Cover = null;
        //    book.Genre = "Drama";
        //    book.Isbn = "23234-234-23-123324";
        //    book.Movie = mov;
        //    book.OriginalTitle = "Seven Pounds";
        //    book.Pending = false;
        //    book.Rating = 5.0;
        //    book.Title = "Sieben Leben";

        //    Actor actor1 = new Actor();
        //    actor1.FirstName = "Will";
        //    actor1.LastName = "Smith";

        //    mov.Actor.Add(actor1);

        //    MovieDaoEF.Instance.Add(mov);
            
        //}



      
    }

}
