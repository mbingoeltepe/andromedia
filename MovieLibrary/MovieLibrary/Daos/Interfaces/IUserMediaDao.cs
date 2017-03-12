using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IUserMediaDao<T> : IGenericDao<T> where T : UserMedia
    {
        IQueryable<T> GetByUserMedia(T media);
        IQueryable<T> GetByUser(string userName);
        void BorrowRequestUserMedia(string usernameTo, T media);
        BorrowedDetails GetBorrowedDetailsById(int id);
        IQueryable<BorrowedDetails> GetAllBorrowedAwayMediaByUser(string username);
        IQueryable<BorrowedDetails> GetAllBorrowedFromMediaByUser(string username);
        void TakeBorrowedMediaBack(string usernameTo, string usernameFrom, T media);
        IQueryable<Media> GetRecommendedMedia(string username);
    }
}
