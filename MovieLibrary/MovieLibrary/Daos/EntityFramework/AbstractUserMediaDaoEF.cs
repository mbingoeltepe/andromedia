using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public abstract class AbstractUserMediaDaoEF<T> : IUserMediaDao<T> where T : UserMedia
    {


        public T GetById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                return (from media in context.UserMediaSet.OfType<T>()
                        where media.Id == id
                        select media).Single();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return context.UserMediaSet.OfType<T>();
        }

        public void Add(T media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.UserMediaSet.AddObject(media);
            context.SaveChanges();
        }

        public void DeleteBorrowRequest(BorrowRequest request)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.BorrowRequestSet.DeleteObject(request);
            context.SaveChanges();
        }

        public void Delete(T media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.UserMediaSet.DeleteObject(media);
            context.SaveChanges();
        }

        public void Save()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            context.SaveChanges();
        }

        public IQueryable<T> GetByUserMedia(T media)
        {
             MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
             var result = from i in context.UserMediaSet.OfType<T>()
                          select i;

            if(media.MediaStatus != null)
            {
                result = result.Where(r => r.MediaStatus.Equals(media.MediaStatus));
            }
            if (!media.StoragePlace.Equals(string.Empty))
            {
                result = result.Where(r => r.StoragePlace.Equals(media.StoragePlace));
            }
            
            //if (media.BorrowedDetails != null)
            //{
            //    var actualDetails = UserMediaDaoEF.Instance.GetAllBorrowedFromMediaByUser(media.BorrowedDetails.ToArray()[0].NameTo);
            //    try
            //    {
            //        //var actualDetail = from i in context.BorrowedDetailsSet
            //        //                   from j in media.BorrowedDetails
            //        //                   where i.NameTo.Equals(j.NameTo) &&
            //        //                   i.DateOfReturn.Date.ToShortDateString().Equals(DateTime.MaxValue.Date.ToShortDateString())
            //        //                   select i;
            //        foreach (var detail in actualDetails)
            //        {
            //            foreach (var res in result)
            //            {
            //                if (res.BorrowedDetails == null)
            //                {
            //                    list.Remove(res);
            //                }
            //                foreach (var borrowDet in res.BorrowedDetails)
            //                {
            //                    if (borrowDet.Id == detail.Id)
            //                    {
            //                        right.Add(res);
            //                    }
            //                }
            //            }
            //            //result = result.Where(r => r.BorrowedDetails.Where(p => p.Id == detail.Id)); // r.BorrowedDetails.Contains(detail));
            //        }
            //        foreach(var res in right)
            //        {
            //            result = result.Where(r => r.Id == res.Id);
            //        }
                    
            //    }
            //    catch (Exception)
            //    { 
                
            //    }
            //}
            if (media.User != null)
            {
                result = result.Where(r => r.UserId == media.UserId);
            }
            //if (media.BorrowedFromDetails.UserFrom != null)
            //{ 
            //    result = result.Where(r => r.BorrowedToDetailsId == media.BorrowedToDetailsId);
            //}
            //TODO

            return result;
        }

        public IQueryable<T> GetByUser(string userName)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return from i in context.UserMediaSet.OfType<T>()
                   where i.User.Username == userName
                   select i;
                         

        }

        #region IUserMediaDao<T> Members


        public void BorrowRequestUserMedia(string usernameTo, T media)
        {
            BorrowRequest request = new BorrowRequest();
            request.User = media.User;
            request.UserMedia = media;
            request.UserTo = usernameTo;

            media.User.BorrowRequest.Add(request);
            Save();
        }


        public BorrowedDetails GetBorrowedDetailsById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return (from details in context.BorrowedDetailsSet
                    where details.Id == id
                    select details).Single();
        }


        public IQueryable<BorrowedDetails> GetAllBorrowedAwayMediaByUser(string username)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var borrowedAway = from i in context.BorrowedDetailsSet
                               where i.UserFrom.Username.Equals(username)
                               select i;
            
            return borrowedAway;

        }


        public IQueryable<BorrowedDetails> GetAllBorrowedFromMediaByUser(string username)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return from i in context.BorrowedDetailsSet
                   where i.NameTo.Equals(username)
                   select i;
        }


        public void TakeBorrowedMediaBack(string usernameTo, string usernameFrom, T media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var detail = (from i in context.BorrowedDetailsSet
                          where i.UserMedia.Equals(media) &&
                          i.UserFrom.Username.Equals(usernameFrom) &&
                          i.NameTo.Equals(usernameTo)
                          select i).Single();
            detail.DateOfReturn = DateTime.Now;
        }

        #endregion


        public IQueryable<Media> GetRecommendedMedia(string username)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var user = MembershipDao.Instance.GetCurrentUser(username);
            var movieIds = from i in user.UserMedien.OfType<UserMovie>()
                            select i.Movie.Id;

            var bookIds = from j in user.UserMedien.OfType<UserBook>()
                          select j.Book.Id;

            var tvShowIds = from i in user.UserMedien.OfType<UserTV_Show>()
                            select i.Season.Id;

            List<int> idList = new List<int>();
            foreach (var id in movieIds)
            {
                idList.Add(id);
            }
            foreach (var id in bookIds)
            {
                idList.Add(id);
            }
            
            var bestMovieGenre = (from i in user.UserMedien.OfType<UserMovie>()
                                  select i.Movie.Genre).Max();

            int genreMovieTotal = (from i in user.UserMedien.OfType<UserMovie>()
                                   where i.Movie.Genre.Equals(bestMovieGenre)
                                   select i.Movie.Genre).Count();

            var bestBookGenre = (from i in user.UserMedien.OfType<UserBook>()
                                  select i.Book.Genre).Max();

            int genreBookTotal = (from i in user.UserMedien.OfType<UserBook>()
                                   where i.Book.Genre.Equals(bestMovieGenre)
                                   select i.Book.Genre).Count();

            var bestTvGenre = (from i in user.UserMedien.OfType<UserTV_Show>()
                                 select i.Season.TV_Show.Genre).Max();

            int genreTvTotal = (from i in user.UserMedien.OfType<UserTV_Show>()
                                where i.Season.TV_Show.Genre.Equals(bestMovieGenre)
                                select i.Season.TV_Show.Genre).Count();

            string bestGenre = string.Empty;

            if ((genreBookTotal >= genreMovieTotal) && (genreBookTotal >= genreTvTotal))
            {
                bestGenre = bestBookGenre;
            }
            else if ((genreMovieTotal >= genreTvTotal) && (genreMovieTotal >= genreBookTotal))
            {
                bestGenre = bestMovieGenre;
            }
            else
            {
                bestGenre = bestTvGenre;
            }

            var newMed = (from i in context.MediaSet
                          where (!idList.Contains(i.Id)) &&
                          (bestGenre.Equals(i.Genre))
                          select i).Take(10);

             return newMed;
        }
    }
}