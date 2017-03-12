using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MovieLibrary.Controllers
{
    public class MovieDao
    {
        public MovieDao()
        {
            MediaSet_Movie movie = new MediaSet_Movie();
            
        }

        public List<MediaSet_Movie> GetAllMovies()
        { 
            ORMapperDataContext dataContext = new ORMapperDataContext();

            var movies = from m in dataContext.MediaSet_Movies
                         select m;

            return movies.ToList();
            
        }
    }
}