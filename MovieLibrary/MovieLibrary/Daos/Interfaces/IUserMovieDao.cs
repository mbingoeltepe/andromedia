using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IUserMovieDao : IUserMediaDao<UserMovie>
    {
        IQueryable<UserMovie> GetUserMovieByMovieId(string username, int videoId);
    }
}