using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using System.Web.Mvc;

namespace MovieLibrary.Service.ServicesImpl
{
    public class UserMediaService : IUserMediaService
    {
        private static readonly UserMediaService instance = new UserMediaService();

        private UserMediaService()
        {
        }

        public static UserMediaService Instance
        {
            get
            {
                return instance;
            }
        }

        public UserBook GetBookById(int id)
        {
            return UserBookDaoEF.Instance.GetById(id);
        }

        public void AddBook(UserBook media)
        {
            UserBookDaoEF.Instance.Add(media);
        }

        public void DeleteBook(UserBook media)
        {
            UserBookDaoEF.Instance.Delete(media);
        }

        public IQueryable<UserBook> GetBookByUserMedia(UserBook media)
        {
            var books = UserBookDaoEF.Instance.GetByUserMedia(media);
            if (!media.Book.Title.Equals(string.Empty))
            {
                books = books.Where(r => r.Book.Title.Contains(media.Book.Title));
            }
            if (media.Book.Author.Count != 0)
            {
                List<UserBook> userBooks = new List<UserBook>();
                foreach (var aut in media.Book.Author)
                {
                    List<Author> authors = PersonService.Instance.GetAuthor(aut.LastName).ToList();
                    foreach (var a in authors)
                    {
                        foreach (var book in books)
                        {
                            if (book.Book.Author.Contains(a))
                            {
                                userBooks.Add(book);
                            }
                        }
                    }
                }
                return userBooks.AsQueryable();
            }
            return books;
        }

        public IQueryable<UserBook> GetAllBooks()
        {
            return UserBookDaoEF.Instance.GetAll();
        }


        public IQueryable<UserBook> GetUserBookByIdAndUserName(string username, int bookId)
        {
            return UserBookDaoEF.Instance.GetUserBooksByBookId(username, bookId);
        }

        public IQueryable<UserTV_Show> GetUserTvShowsByUserName(string username)
        {
            return UserTvShowDaoEF.Instance.GetByUser(username);
        }

        public IQueryable<UserMovie> GetUserMoviesByUserName(string username)
        {
            return UserMovieDaoEF.Instance.GetByUser(username);
        }

        public IQueryable<UserMovie> GetUserMovieByIdAndUserName(string username, int videoId)
        {
            return UserMovieDaoEF.Instance.GetUserMovieByMovieId(username, videoId);
        }

        public IQueryable<UserTV_Show> GetUserTvShowByIdAndUserName(string username, int videoId)
        {
            return UserTvShowDaoEF.Instance.GetUserTvShowByTvShowId(username, videoId);
        }

        public IQueryable<UserMovie> GetAllMovies()
        {
            var allVids = UserMovieDaoEF.Instance.GetAll();
            return from movies in allVids
                   select movies;
        }

        public IQueryable<UserTV_Show> GetAllTvShows()
        {
            var allVids = UserTvShowDaoEF.Instance.GetAll();
            return from movies in allVids
                   select movies;
        }

        public void AddMovie(UserMovie media)
        {
            UserMovieDaoEF.Instance.Add(media);
        }

        public void AddTvShow(UserTV_Show media)
        {
            UserTvShowDaoEF.Instance.Add(media);
        }

        public void DeleteMovie(UserMovie media)
        {
            UserMovieDaoEF.Instance.Delete(media);
        }

        public void DeleteTvShow(UserTV_Show media)
        {
            UserTvShowDaoEF.Instance.Delete(media);
        }



        public IQueryable<UserBook> GetUserBooksByUserName(string username)
        {
            return UserBookDaoEF.Instance.GetByUser(username);
        }
        
        public UserMovie GetMovieById(int id)
        {
            return UserMovieDaoEF.Instance.GetById(id);
        }

        public UserTV_Show GetTvShowById(int id)
        {
            return UserTvShowDaoEF.Instance.GetById(id);
        }

        public IQueryable<UserTV_Show> GetTvShowByUserMedia(UserTV_Show media)
        {
            var medien = UserTvShowDaoEF.Instance.GetByUserMedia(media);

            if (!media.Season.TV_Show.Title.Equals(string.Empty))
            {
                medien = medien.Where(r => r.Season.TV_Show.Title.Contains(media.Season.TV_Show.Title));
            }
            if (media.Season.Number != 0)
            {
                medien = medien.Where(r => r.Season.Number == media.Season.Number);
            }
            if (!media.StorageType.Equals(string.Empty))
            {
                medien = medien.Where(r => r.StorageType.Equals(media.StorageType));
            }
            return medien;
        }

        public IQueryable<UserMovie> GetMovieByUserMedia(UserMovie media)
        {
            var medien = UserMovieDaoEF.Instance.GetByUserMedia(media);
            if (!media.Movie.Title.Equals(string.Empty))
            {
                medien = medien.Where(r => r.Movie.Title.Contains(media.Movie.Title));
            }
            if (!media.StorageType.Equals(string.Empty))
            {
                medien = medien.Where(r => r.StorageType.Equals(media.StorageType));
            }
            return medien;
        }


        public Season GetUserSeasonById(int id)
        {
            return UserTvShowDaoEF.Instance.GetSeasonById(id);
        }

        #region IUserMediaService Members


        public void BorrowRequestUserMedia(string usernameTo, UserMedia media)
        {
            UserMediaDaoEF.Instance.BorrowRequestUserMedia(usernameTo, media);
        }

        #endregion

        #region IUserMediaService Members


        public UserMedia GetUserMediaById(int id)
        {
            return UserMediaDaoEF.Instance.GetById(id);
        }

        #endregion

        #region IUserMediaService Members


        public BorrowedDetails GetBorrowedDetailsById(int id)
        {
            return UserMediaDaoEF.Instance.GetBorrowedDetailsById(id);
        }

        #endregion

        #region IUserMediaService Members


        public void Save()
        {
            UserMediaDaoEF.Instance.Save();
        }

        #endregion

        #region IUserMediaService Members


        public void BorrowUserMediaToUser(string usernameTo, UserMedia media)
        {
            BorrowedDetails details = new BorrowedDetails();
            
            details.NameTo = usernameTo;
            details.BorrowedFromUserId = media.UserId;
            details.Date = DateTime.Now;
            details.DateOfReturn = DateTime.MaxValue;
            details.UserFrom = media.User;
            
            details.UserMedia = media;
            
            media.MediaStatus = Models.UserMediaStatusEnum.Verborgt.ToString();

            try
            {
                var request = (from i in media.BorrowRequest
                               where i.UserTo.Equals(usernameTo)
                               select i).Single();
                
                media.BorrowRequest.Remove(request);
                UserMediaDaoEF.Instance.DeleteBorrowRequest(request);
            }
            catch (Exception)
            {

            }
            Save();
        }

        public void DiscardBorrowUserMediaToUser(string usernameTo, UserMedia media)
        {
            try
            {
                var request = (from i in media.BorrowRequest
                               where i.UserTo.Equals(usernameTo)
                               select i).Single();

                media.BorrowRequest.Remove(request);
                UserMediaDaoEF.Instance.DeleteBorrowRequest(request);
            }
            catch (Exception)
            {

            }
            Save();
        }

        public IQueryable<BorrowedDetails> GetAllBorrowedAwayMediaByUser(string username)
        {
            return UserMediaDaoEF.Instance.GetAllBorrowedAwayMediaByUser(username);
        }

        public IQueryable<BorrowedDetails> GetAllBorrowedFromMediaByUser(string username)
        {
            return UserMediaDaoEF.Instance.GetAllBorrowedFromMediaByUser(username);
        }

        #endregion


        public IQueryable<MediaImageUserViewModel> GetRecommendedMedia(string username)
        {
            try
            {
                List<MediaImageUserViewModel> recommendedList = new List<MediaImageUserViewModel>();
                AWSImageService images = new AWSImageService();
                var media = UserMediaDaoEF.Instance.GetRecommendedMedia(username);
                var user = MembershipService.Instance.GetCurrentUser(username);
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
                        MediaImageUserViewModel recommended = new MediaImageUserViewModel(med, image, user);
                        recommendedList.Add(recommended);
                    }
                }
                return recommendedList.AsQueryable<MediaImageUserViewModel>();
            }
            catch (Exception)
            {
                return new List<MediaImageUserViewModel>().AsQueryable<MediaImageUserViewModel>();
            }
        }
    }
}