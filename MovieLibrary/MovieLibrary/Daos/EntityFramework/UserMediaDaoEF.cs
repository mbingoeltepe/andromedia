using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.EntityFramework
{
    public class UserMediaDaoEF:AbstractUserMediaDaoEF<UserMedia>
    {
        private static readonly UserMediaDaoEF instance = new UserMediaDaoEF();

        private UserMediaDaoEF()
        {
        }

        public static UserMediaDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
    }
}