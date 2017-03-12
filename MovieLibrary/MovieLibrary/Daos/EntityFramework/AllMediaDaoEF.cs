using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.EntityFramework
{
    public class AllMediaDaoEF:AbstractMediaDaoEF<Media>
    {

        private static readonly AllMediaDaoEF instance = new AllMediaDaoEF();

        private AllMediaDaoEF()
        {
        }

        public static AllMediaDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}