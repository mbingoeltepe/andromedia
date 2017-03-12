using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IMediaService
    {
        #region Movies
        
        Movie GetMovieById(int id);
        IQueryable<Movie> GetAllMovies();
        void AddMovie(Movie media);
        void DeleteMovie(Movie media);
        IQueryable<Movie> GetMovieByTitle(string titleOrOriginaltitle);
        List<Movie> GetMovieByQuoteString(string quoteString);
        IQueryable<Book> GetBooksForMovie(int id);
        Movie GetMovieByExactTitle(string title);
        void SaveChangesMovie();
    
        #endregion
        #region TvShows

        TV_Show GetTvShowById(int id);
        IQueryable<TV_Show> GetAllTvShows();
        void AddTvShow(TV_Show media);
        void DeleteTvShow(TV_Show media);
        IQueryable<TV_Show> GetTvShowByTitle(string titleOrOriginaltitle);
        List<TV_Show> GetTvShowByQuoteString(string quoteString);
        Season GetSeasonById(int id);
        void SaveChangesTvShow();
        void DeleteSeason(Season season);

        #endregion
        #region Books

        Book GetBookById(int id);
        IQueryable<Book> GetAllBooks();
        void AddBook(Book media);
        void DeleteBook(Book media);
        IQueryable<Book> GetBookByTitle(string titleOrOriginaltitle);
        List<Book> GetBookByQuoteString(string quoteString);
        Book GetBookByExactTitle(string title);
        void SaveChangesBook();
        IQueryable<Movie> GetMoviesForBook(int id);

        #endregion
        #region Media

        Media GetMediaById(int id);
        IQueryable<Media> GetAllMedia();
        void AddMedia(Media media);
        void DeleteMedia(Media media);
        IQueryable<Media> GetMediaByTitle(string titleOrOriginaltitle);
        List<Media> GetMediaByQuoteString(string quoteString);
        IQueryable<MediaImageUserViewModel> GetLast10AddedTitles();
        IQueryable<Media> GetTopRatedMedia();

        #endregion
    }
}
