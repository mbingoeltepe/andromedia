using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IMediaDao<T> : IGenericDao<T> where T : Media
    {
        IQueryable<T> GetByTitle(string titleOrOriginaltitle);
        IQueryable<T> GetLast10AddedTitles();
        IQueryable<T> GetTopRatedMedia();
    }
}