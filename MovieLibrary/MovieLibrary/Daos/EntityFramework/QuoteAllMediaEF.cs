using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.EntityFramework
{
    public class QuoteAllMediaEF : AbstractQuoteDaoEF<Quote>
    {
        private static readonly QuoteAllMediaEF instance = new QuoteAllMediaEF();

        private QuoteAllMediaEF()
        {
        }

        public static QuoteAllMediaEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}