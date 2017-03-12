using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class UserMovieDaoEF : AbstractUserMediaDaoEF<UserMovie>, IUserMovieDao
    {

        private static readonly UserMovieDaoEF instance = new UserMovieDaoEF();

        private UserMovieDaoEF()
        {
        }

        public static UserMovieDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
        public IQueryable<UserMovie> GetUserMovieByMovieId(string username, int videoId)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            var userMovies = from userMovie in context.UserMediaSet.OfType<UserMovie>()
                             where userMovie.Movie.Id == videoId && userMovie.User.Username == username
                             select userMovie;

            return userMovies;
        }
    }
}