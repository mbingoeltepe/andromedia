using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Service;

namespace MovieLibrary.Models
{
    public class TVShowDetailsViewModel : AbstractMediaDetailsViewModel<TV_Show, UserTV_Show>
    {
        public IEnumerable<SelectListItem> Seasons { get; protected set; }

        public TVShowDetailsViewModel(TV_Show tvShow, Image image, IQueryable<UserTV_Show> userTvShows, User user)
            : base(tvShow, image, userTvShows, user)
        {
            IOrderedEnumerable<Season> orderedSeason = tvShow.Season.OrderBy(season => season.Number); 

            Seasons =
                from s in orderedSeason
                select new SelectListItem
                {
                    Text = s.Number.ToString(),
                    Value = s.Id.ToString()
                };
        }

        public TVShowDetailsViewModel(TV_Show tvShow, Image image)
            : base(tvShow, image)
        {
        }
    }
}