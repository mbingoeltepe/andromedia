using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public sealed class BookDaoEF : AbstractMediaDaoEF<Book>, IBookDao
    {
        private static readonly BookDaoEF instance = new BookDaoEF();

        private BookDaoEF()
        {
        }

        public static BookDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        public IQueryable<Movie> FindMoviesForBook(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            Book book;

            try
            {

                book = (from i in context.MediaSet.OfType<Book>()
                            where id == i.Id
                            select i).Single();
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            var movies = from i in context.MediaSet.OfType<Movie>()
                        where book.Title.Contains(i.Title) ||
                        i.Title.Contains(book.Title) ||
                        i.OriginalTitle.Contains(book.OriginalTitle) ||
                        book.OriginalTitle.Contains(i.OriginalTitle)
                        select i;

            return movies;
        }

    }
}