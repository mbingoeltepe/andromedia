using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public class AuthorDaoEF : AbstractPersonDaoEF<Author>, IAuthorDao
    {
        private static readonly AuthorDaoEF instance = new AuthorDaoEF();

        private AuthorDaoEF()
        {
        }

        public static AuthorDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        #region IAuthorDao Members
        public IQueryable<Author> GetAuthor(string name)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                var auth = from author in context.PersonSet.OfType<Author>()
                           where name.Contains(author.FirstName) &&
                           name.Contains(author.LastName)
                           select author;
                if (auth.Count() == 0)
                {
                    auth = from author in context.PersonSet.OfType<Author>()
                           where name.Contains(author.FirstName) ||
                           name.Contains(author.LastName)
                           select author;
                }
                return auth;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region IAuthorDao Members


        public Author GetAuthorByFullName(string firstname, string lastname)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                var auth = from author in context.PersonSet.OfType<Author>()
                          where firstname.Equals(author.FirstName) &&
                          lastname.Equals(author.LastName)
                          select author;
                return auth.Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}