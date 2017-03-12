using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IBookDao : IMediaDao<Book>
    {
        IQueryable<Movie> FindMoviesForBook(int id);
    }
}