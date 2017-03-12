using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class UserTvShowDaoEF : AbstractUserMediaDaoEF<UserTV_Show>, IUserTvShowDao
    {

        private static readonly UserTvShowDaoEF instance = new UserTvShowDaoEF();

        private UserTvShowDaoEF()
        {
        }

        public static UserTvShowDaoEF Instance
        {
            get
            {
                return instance;
            }
        }
        public IQueryable<UserTV_Show> GetUserTvShowByTvShowId(string username, int videoId)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            var userTvShows = from userTvShow in context.UserMediaSet.OfType<UserTV_Show>()
                             where userTvShow.Season.TV_Show.Id == videoId && userTvShow.User.Username == username
                             select userTvShow;

            return userTvShows;
        }


        public Season GetSeasonById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return (from userSeason in context.UserMediaSet.OfType<UserTV_Show>()
                    where userSeason.SeasonId == id
                    select userSeason.Season).Single();
        }

    }
}