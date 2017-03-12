using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class QuoteMovieDaoEF : AbstractQuoteDaoEF<QuoteMovie>, IQuoteMovieDao
    {
        private static readonly QuoteMovieDaoEF instance = new QuoteMovieDaoEF();

        private QuoteMovieDaoEF()
        {
        }

        public static QuoteMovieDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}