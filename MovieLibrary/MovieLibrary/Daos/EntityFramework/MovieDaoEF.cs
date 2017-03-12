using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;


namespace MovieLibrary.Daos.EntityFramework
{
    public sealed class MovieDaoEF : AbstractMediaDaoEF<Movie>, IMovieDao
    {
        private static readonly MovieDaoEF instance = new MovieDaoEF();

        private MovieDaoEF()
        {
        }

        public static MovieDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        public IQueryable<Book> FindBooksForMovie(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            Movie movie;

            try
            {
                movie = (from i in context.MediaSet.OfType<Movie>()
                         where id == i.Id
                         select i).Single();
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            var books = from i in context.MediaSet.OfType<Book>()
                        where movie.Title.Contains(i.Title) ||
                        i.Title.Contains(movie.Title) ||
                        i.OriginalTitle.Contains(movie.OriginalTitle) ||
                        movie.OriginalTitle.Contains(i.OriginalTitle)
                        select i;

            return books;
        }
    }
}