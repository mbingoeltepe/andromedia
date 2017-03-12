using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class QuoteTV_ShowDaoEF : AbstractQuoteDaoEF<QuoteTV_Show>, IQuoteTVShowDao
    {
        private static readonly QuoteTV_ShowDaoEF instance = new QuoteTV_ShowDaoEF();

        private QuoteTV_ShowDaoEF()
        {
        }

        public static QuoteTV_ShowDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}