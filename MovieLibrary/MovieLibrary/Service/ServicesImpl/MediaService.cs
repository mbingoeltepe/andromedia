using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;

namespace MovieLibrary.Service.ServicesImpl
{
    public class MediaService:IMediaService
    {
        private static readonly MediaService instance = new MediaService();

        private MediaService()
        {
        }

        public static MediaService Instance
        {
            get 
            {
                return instance;
            }
        }

        public Models.Movie GetMovieById(int id)
        {
            return MovieDaoEF.Instance.GetById(id);
        }

        public IQueryable<Models.Movie> GetAllMovies()
        {
            return MovieDaoEF.Instance.GetAll();
        }

        public void AddMovie(Models.Movie media)
        {
            MovieDaoEF.Instance.Add(media);
        }

        public void DeleteMovie(Models.Movie media)
        {
            MovieDaoEF.Instance.Delete(media);
        }

        public IQueryable<Models.Movie> GetMovieByTitle(string titleOrOriginaltitle)
        {
            return MovieDaoEF.Instance.GetByTitle(titleOrOriginaltitle);
        }

        public Models.TV_Show GetTvShowById(int id)
        {
            return TV_ShowDaoEF.Instance.GetById(id);
        }

        public IQueryable<Models.TV_Show> GetAllTvShows()
        {
            return TV_ShowDaoEF.Instance.GetAll();
        }

        public void AddTvShow(Models.TV_Show media)
        {
            TV_ShowDaoEF.Instance.Add(media);
        }

        public void DeleteTvShow(Models.TV_Show media)
        {
            TV_ShowDaoEF.Instance.Delete(media);
        }

        public IQueryable<Models.TV_Show> GetTvShowByTitle(string titleOrOriginaltitle)
        {
            return TV_ShowDaoEF.Instance.GetByTitle(titleOrOriginaltitle);
        }

        public Models.Book GetBookById(int id)
        {
            return BookDaoEF.Instance.GetById(id);
        }

        public IQueryable<Models.Book> GetAllBooks()
        {
            return BookDaoEF.Instance.GetAll();
        }

        public void AddBook(Models.Book media)
        {
            BookDaoEF.Instance.Add(media);
        }

        public void DeleteBook(Models.Book media)
        {
            BookDaoEF.Instance.Delete(media);
        }

        public IQueryable<Models.Book> GetBookByTitle(string titleOrOriginaltitle)
        {
            return BookDaoEF.Instance.GetByTitle(titleOrOriginaltitle);
        }
        
        public Models.Media GetMediaById(int id)
        {
            return AllMediaDaoEF.Instance.GetById(id);
        }

        public IQueryable<Models.Media> GetAllMedia()
        {
            return AllMediaDaoEF.Instance.GetAll();
        }

        public void AddMedia(Models.Media media)
        {
            AllMediaDaoEF.Instance.Add(media);
        }

        public void DeleteMedia(Models.Media media)
        {
            AllMediaDaoEF.Instance.Delete(media);
        }

        public IQueryable<Models.Media> GetMediaByTitle(string titleOrOriginaltitle)
        {
            return AllMediaDaoEF.Instance.GetByTitle(titleOrOriginaltitle);
        }


        public List<Models.Movie> GetMovieByQuoteString(string quoteString)
        {
            var result = QuoteMovieDaoEF.Instance.GetByQuoteString(quoteString);

            
            List<Movie> mediaList = new List<Movie>();

            IEnumerable<Quote> rank = result.OrderBy(r => r.Ranking);
            foreach (var medium in rank)
            {
                mediaList.Add(medium.Media as Movie);
            }
            return mediaList;
        }

        public List<Models.TV_Show> GetTvShowByQuoteString(string quoteString)
        {
            var result = QuoteTV_ShowDaoEF.Instance.GetByQuoteString(quoteString);

            List<TV_Show> mediaList = new List<TV_Show>();

            IEnumerable<Quote> rank = result.OrderBy(r => r.Ranking);
            foreach (var medium in rank)
            {
                mediaList.Add(medium.Media as TV_Show);
            }
            return mediaList;
        }

        public List<Models.Book> GetBookByQuoteString(string quoteString)
        {
            var result = QuoteBookDaoEF.Instance.GetByQuoteString(quoteString);

            List<Book> mediaList = new List<Book>();

            IEnumerable<Quote> rank = result.OrderBy(r => r.Ranking);
            foreach (var medium in rank)
            {
                mediaList.Add(medium.Media as Book);
            }
            return mediaList;
        }

        public List<Models.Media> GetMediaByQuoteString(string quoteString)
        {
            var result = QuoteAllMediaEF.Instance.GetByQuoteString(quoteString);

            List<Media> mediaList = new List<Media>();

            IEnumerable<Quote> rank = result.OrderBy(r => r.Ranking);
            foreach (var medium in rank)
            {
                mediaList.Add(medium.Media);
            }
            return mediaList;
        }


        public Season GetSeasonById(int id)
        {
            return TV_ShowDaoEF.Instance.GetSeason(id);
        }


        public IQueryable<Book> GetBooksForMovie(int id)
        {
            return MovieDaoEF.Instance.FindBooksForMovie(id);
        }


        public Book GetBookByExactTitle(string title)
        {
            var books = BookDaoEF.Instance.GetByTitle(title);
            foreach (var b in books)
            {
                if (b.Title.Equals(title))
                {
                    return b;
                }
            }
            return null;
        }

        public void SaveChangesMovie()
        {
            MovieDaoEF.Instance.Save();
        }

        public void SaveChangesTvShow()
        {
            TV_ShowDaoEF.Instance.Save();
        }

        public void SaveChangesBook()
        {
            BookDaoEF.Instance.Save();
        }


        public Movie GetMovieByExactTitle(string title)
        {
            var movies = MovieDaoEF.Instance.GetByTitle(title);
            foreach (var m in movies)
            {
                if (m.Title.Equals(title))
                {
                    return m;
                }
            }
            return null;
        }


        public IQueryable<Movie> GetMoviesForBook(int id)
        {
            return BookDaoEF.Instance.FindMoviesForBook(id);
        }

        public void DeleteSeason(Season season)
        {
            TV_ShowDaoEF.Instance.DeleteSeason(season);
        }


        public IQueryable<MediaImageUserViewModel> GetLast10AddedTitles()
        {
            try
            {
                List<MediaImageUserViewModel> lastAddedList = new List<MediaImageUserViewModel>();
                AWSImageService images = new AWSImageService();
                var media = AllMediaDaoEF.Instance.GetLast10AddedTitles();
                Image image = null;
                foreach (var med in media)
                {
                    if (med.GetType() == typeof(Movie) || med.GetType() == typeof(TV_Show))
                    {
                        image = images.GetImagesForVideo((Video)med)[AWSImageService.MEDIUMIMAGE];
                    }
                    else if (med.GetType() == typeof(Book))
                    {
                        image = images.GetImagesForBook((Book)med)[AWSImageService.MEDIUMIMAGE];
                    }
                    if (image != null)
                    {
                        MediaImageUserViewModel recommended = new MediaImageUserViewModel(med, image, null);
                        lastAddedList.Add(recommended);
                    }
                }
                return lastAddedList.AsQueryable<MediaImageUserViewModel>();
            }
            catch (Exception)
            {
                return new List<MediaImageUserViewModel>().AsQueryable<MediaImageUserViewModel>();
            }
        }

        public IQueryable<Media> GetTopRatedMedia()
        {
            return AllMediaDaoEF.Instance.GetTopRatedMedia();
        }
    }
}