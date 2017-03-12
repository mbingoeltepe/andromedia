using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public sealed class TV_ShowDaoEF : AbstractMediaDaoEF<TV_Show>, ITVShowDao
    {
        private static readonly TV_ShowDaoEF instance = new TV_ShowDaoEF();

        private TV_ShowDaoEF()
        {
        }

        public static TV_ShowDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        public Season GetSeason(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return (from i in context.SeasonSet
                    where i.Id == id
                    select i).Single();
                   
        }

        public void DeleteSeason(Season season)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.DeleteObject(season);
        }
    }
}