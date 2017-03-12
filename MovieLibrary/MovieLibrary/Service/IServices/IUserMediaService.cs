using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IUserMediaService
    {

        IQueryable<UserTV_Show> GetUserTvShowsByUserName(string username);
        IQueryable<UserMovie> GetUserMoviesByUserName(string username);
        IQueryable<UserBook> GetUserBooksByUserName(string username);
        IQueryable<UserMovie> GetUserMovieByIdAndUserName(string username, int videoId);
        IQueryable<UserTV_Show> GetUserTvShowByIdAndUserName(string username, int videoId);
        IQueryable<UserBook> GetUserBookByIdAndUserName(string username, int bookId);

        UserMedia GetUserMediaById(int id);

        UserMovie GetMovieById(int id);
        UserTV_Show GetTvShowById(int id);
        Season GetUserSeasonById(int id);
        UserBook GetBookById(int id);
        IQueryable<UserMovie> GetAllMovies();
        IQueryable<UserTV_Show> GetAllTvShows();
        IQueryable<UserBook> GetAllBooks();
        void AddBook(UserBook media);
        void AddMovie(UserMovie media);
        void AddTvShow(UserTV_Show media);
        void DeleteBook(UserBook media);
        void DeleteMovie(UserMovie media);
        void DeleteTvShow(UserTV_Show media);

        IQueryable<UserTV_Show> GetTvShowByUserMedia(UserTV_Show media);
        IQueryable<UserMovie> GetMovieByUserMedia(UserMovie media);
        IQueryable<UserBook> GetBookByUserMedia(UserBook media);

        void BorrowRequestUserMedia(string usernameTo, UserMedia media);
        BorrowedDetails GetBorrowedDetailsById(int id);
        void BorrowUserMediaToUser(string usernameTo, UserMedia media);
        void DiscardBorrowUserMediaToUser(string usernameTo, UserMedia media);
        IQueryable<BorrowedDetails> GetAllBorrowedAwayMediaByUser(string username);
        IQueryable<BorrowedDetails> GetAllBorrowedFromMediaByUser(string username);

        IQueryable<MediaImageUserViewModel> GetRecommendedMedia(string username);

        void Save();
    }
}