using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class QuoteBookDaoEF : AbstractQuoteDaoEF<QuoteBook>, IQuoteBookDao
    {
        private static readonly QuoteBookDaoEF instance = new QuoteBookDaoEF();

        private QuoteBookDaoEF()
        {
        }

        public static QuoteBookDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}