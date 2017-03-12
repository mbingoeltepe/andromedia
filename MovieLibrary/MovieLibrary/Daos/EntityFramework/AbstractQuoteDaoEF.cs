using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public class AbstractQuoteDaoEF<T> : IQuoteDao<T> where T : Quote
    {
        public T GetById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                return (from quote in context.QuoteSet.OfType<T>()
                        where quote.Id == id
                        select quote).Single();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return context.QuoteSet.OfType<T>().Where(q => q.Ranking > 0);
        }


        public IQueryable<T> GetAllNotRanking(string userName, string language, string title)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            IQueryable<T> result = from quote in context.QuoteSet.OfType<T>()
                                   where quote.Ranking == 0 &&
                                   quote.User.Username.Contains(userName) &&
                                   quote.Language.Contains(language) &&
                                   quote.Media.Title.Contains(title)
                                   select quote;


            return result;
        }



        public IQueryable<T> GetByQuoteString(string quoteString)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            IQueryable<T> result = from quote in context.QuoteSet.OfType<T>()
                                   where quote.QuoteString.Contains(quoteString)                                    
                                   select quote;

            foreach(T quote in result)
                quote.Invocations++;

            context.SaveChanges();

            return result;
        }

        public IQueryable<T> GetByQuoteStringRanked(string quoteString)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return GetByQuoteString(quoteString).Where(quote => quote.Ranking > 0).OrderBy(quote => quote.Ranking);
        }

        public void Add(T quote)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.QuoteSet.AddObject(quote);
            context.SaveChanges();
        }

        public void Delete(T quote)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.QuoteSet.DeleteObject(quote);
            context.SaveChanges();
        }

        public void Save()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.SaveChanges();
        }
    }
}