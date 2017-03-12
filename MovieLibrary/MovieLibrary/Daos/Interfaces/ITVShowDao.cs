using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface ITVShowDao : IMediaDao<TV_Show>
    {
        Season GetSeason(int id);
        void DeleteSeason(Season season);
    }
}