using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class UserBookDaoEF : AbstractUserMediaDaoEF<UserBook>, IUserBookDao
    {

        private static readonly UserBookDaoEF instance = new UserBookDaoEF();

        private UserBookDaoEF()
        {
        }

        public static UserBookDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        public IQueryable<UserBook> GetUserBooksByBookId(string username, int bookId)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            var userBooks = from userBook in context.UserMediaSet.OfType<UserBook>()
                            where userBook.Book.Id == bookId && userBook.User.Username == username
                            select userBook;

            return userBooks;
        }
    }
}