using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;

namespace MovieLibrary.Daos
{
    public class AllQuotesDaoEF : AbstractQuoteDaoEF<Quote>
    {
        private static readonly AllQuotesDaoEF instance = new AllQuotesDaoEF();

        private AllQuotesDaoEF()
        {
        }

        public static AllQuotesDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}