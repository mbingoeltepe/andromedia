using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IQuoteDao<T> : IGenericDao<T> where T : Quote
    {
        IQueryable<T> GetByQuoteString(string quoteString);
        IQueryable<T> GetByQuoteStringRanked(string quoteString);
    }
}
