using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IUserBookDao : IUserMediaDao<UserBook>
    {
        IQueryable<UserBook> GetUserBooksByBookId(string username, int bookId);
    }
}