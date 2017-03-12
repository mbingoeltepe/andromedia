using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Daos;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;

namespace MovieLibrary.Service.ServicesImpl
{
    public partial class AllQuotesService : IQuoteService<Quote>
    {
        private static readonly AllQuotesService instance = new AllQuotesService();

        private AllQuotesService()
        {
        }

        public static AllQuotesService Instance
        {
            get
            {
                return instance;
            }
        }

        public Quote GetQuoteById(int id)
        {
            return AllQuotesDaoEF.Instance.GetById(id);
        }

        public IQueryable<Quote> GetAllQuotes()
        {
            return AllQuotesDaoEF.Instance.GetAll();
        }

        public IQueryable<Quote> GetAllNotRankingQuotes(string userName, string language, string title)
        {
            return AllQuotesDaoEF.Instance.GetAllNotRanking(userName,language,title);
        }


        public void AddQuote(Quote quote)
        {
            quote.Ranking = 0;
            AllQuotesDaoEF.Instance.Add(quote);
        }

        public void DeleteQuote(Quote quote)
        {
            AllQuotesDaoEF.Instance.Delete(quote);
        }

        public void SaveQuote()
        {
            AllQuotesDaoEF.Instance.Save();
        }

        public IQueryable<Quote> GetQuoteByQuoteString(string quoteString)
        {
            return AllQuotesDaoEF.Instance.GetByQuoteString(quoteString);
        }

        public IQueryable<Quote> GetQuoteByQuoteStringRanked(string quoteString)
        {
            return AllQuotesDaoEF.Instance.GetByQuoteStringRanked(quoteString);
        }
    }

    public partial class QuoteBookService : IQuoteService<QuoteBook>
    {
        private static readonly QuoteBookService instance = new QuoteBookService();

        public static QuoteBookService Instance
        {
            get
            {
                return instance;
            }
        }

        public Quote GetQuoteById(int id)
        {
            return QuoteBookDaoEF.Instance.GetById(id);
        }

        public IQueryable<QuoteBook> GetAllQuotes()
        {
            return QuoteBookDaoEF.Instance.GetAll();
        }

        public IQueryable<QuoteBook> GetAllNotRankingQuotes(string userName, string language, string title)
        {
            return QuoteBookDaoEF.Instance.GetAllNotRanking(userName, language, title);
        }

        public void AddQuote(QuoteBook quote)
        {
            QuoteBookDaoEF.Instance.Add(quote);
        }

        public void DeleteQuote(QuoteBook quote)
        {
            QuoteBookDaoEF.Instance.Delete(quote);
        }

        public void SaveQuote()
        {
            QuoteBookDaoEF.Instance.Save();
        }

        public IQueryable<QuoteBook> GetQuoteByQuoteString(string quoteString)
        {
            return QuoteBookDaoEF.Instance.GetByQuoteString(quoteString);
        }

        public IQueryable<QuoteBook> GetQuoteByQuoteStringRanked(string quoteString)
        {
            return QuoteBookDaoEF.Instance.GetByQuoteStringRanked(quoteString);
        }
    }

    public partial class QuoteMovieService : IQuoteService<QuoteMovie>
    {
        private static readonly QuoteMovieService instance = new QuoteMovieService();

        public static QuoteMovieService Instance
        {
            get
            {
                return instance;
            }
        }

        public Quote GetQuoteById(int id)
        {
            return QuoteMovieDaoEF.Instance.GetById(id);
        }

        public IQueryable<QuoteMovie> GetAllQuotes()
        {
            return QuoteMovieDaoEF.Instance.GetAll();
        }

        public IQueryable<QuoteMovie> GetAllNotRankingQuotes(string userName, string language, string title)
        {
            return QuoteMovieDaoEF.Instance.GetAllNotRanking(userName, language, title);
        }

        public void AddQuote(QuoteMovie quote)
        {
            QuoteMovieDaoEF.Instance.Add(quote);
        }

        public void DeleteQuote(QuoteMovie quote)
        {
            QuoteMovieDaoEF.Instance.Delete(quote);
        }

        public void SaveQuote()
        {
            QuoteMovieDaoEF.Instance.Save();
        }

        public IQueryable<QuoteMovie> GetQuoteByQuoteString(string quoteString)
        {
            return QuoteMovieDaoEF.Instance.GetByQuoteString(quoteString);
        }

        public IQueryable<QuoteMovie> GetQuoteByQuoteStringRanked(string quoteString)
        {
            return QuoteMovieDaoEF.Instance.GetByQuoteStringRanked(quoteString);
        }
    }

    public partial class QuoteTvShowService : IQuoteService<QuoteTV_Show>
    {
        private static readonly QuoteTvShowService instance = new QuoteTvShowService();

        public static QuoteTvShowService Instance
        {
            get
            {
                return instance;
            }
        }

        public Quote GetQuoteById(int id)
        {
            return QuoteTV_ShowDaoEF.Instance.GetById(id);
        }

        public IQueryable<QuoteTV_Show> GetAllQuotes()
        {
            return QuoteTV_ShowDaoEF.Instance.GetAll();
        }

        public IQueryable<QuoteTV_Show> GetAllNotRankingQuotes(string userName, string language, string title)
        {
            return QuoteTV_ShowDaoEF.Instance.GetAllNotRanking(userName, language, title);
        }

        public void AddQuote(QuoteTV_Show quote)
        {
            QuoteTV_ShowDaoEF.Instance.Add(quote);
        }

        public void DeleteQuote(QuoteTV_Show quote)
        {
            QuoteTV_ShowDaoEF.Instance.Delete(quote);
        }

        public void SaveQuote()
        {
            QuoteTV_ShowDaoEF.Instance.Save();
        }

        public IQueryable<QuoteTV_Show> GetQuoteByQuoteString(string quoteString)
        {
            return QuoteTV_ShowDaoEF.Instance.GetByQuoteString(quoteString);
        }

        public IQueryable<QuoteTV_Show> GetQuoteByQuoteStringRanked(string quoteString)
        {
            return QuoteTV_ShowDaoEF.Instance.GetByQuoteStringRanked(quoteString);
        }
    }
}