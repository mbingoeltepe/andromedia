using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service;

namespace MovieLibrary.Models
{
    public class MovieDetailsViewModel : AbstractMediaDetailsViewModel<Movie, UserMovie>
    {
        public MovieDetailsViewModel(Movie movie, Image image, IQueryable<UserMovie> userMovies, User user) : base(movie, image, userMovies, user)
        {
        }

        public MovieDetailsViewModel(Movie movie, Image image) : base(movie, image)
        {
        }
    }
}