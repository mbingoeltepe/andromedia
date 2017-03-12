using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Helpers
{
    public class PaginatedTopQuotesList : List<TopQuotesViewModel>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedTopQuotesList(IQueryable<Quote> source, int pageIndex, int pageSize) 
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int) Math.Ceiling(TotalCount / (double)PageSize);

            AddTopQuotesViewModels(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public bool HasPreviousPage 
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        private void AddTopQuotesViewModels(IQueryable<Quote> source)
        {
            foreach (Quote quote in source)
            {
                if (quote.GetType().Equals(typeof(QuoteMovie)))
                {
                    this.Add(new TopQuotesViewModel(quote, (quote as QuoteMovie).Movie));
                }
                else if (quote.GetType().Equals(typeof(QuoteBook)))
                {
                    this.Add(new TopQuotesViewModel(quote, (quote as QuoteBook).Book));
                }
                else if (quote.GetType().Equals(typeof(QuoteTV_Show)))
                {
                    this.Add(new TopQuotesViewModel(quote, (quote as QuoteTV_Show).Episode.Season.TV_Show));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}