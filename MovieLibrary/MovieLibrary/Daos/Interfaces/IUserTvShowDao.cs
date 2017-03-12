using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    interface IUserTvShowDao : IUserMediaDao<UserTV_Show>
    {
        IQueryable<UserTV_Show> GetUserTvShowByTvShowId(string username, int videoId);
        Season GetSeasonById(int id);
    }
}
