using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IQuoteService<T> where T : Quote
    {
        Quote GetQuoteById(int id);
        IQueryable<T> GetAllQuotes();
        IQueryable<T> GetAllNotRankingQuotes(string userName, string language, string title);
        void AddQuote(T quote);
        void DeleteQuote(T quote);
        void SaveQuote();
        IQueryable<T> GetQuoteByQuoteString(string quoteString);
        IQueryable<T> GetQuoteByQuoteStringRanked(string quoteString);
    }
}
