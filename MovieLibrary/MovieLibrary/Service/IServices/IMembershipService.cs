using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IMembershipService
    {
        bool RegisterUser(string mailAddress, string password);
        bool VerifyUser(string mailAddress, string verificationCode);
        bool ChangePassword(string username, string password);
        User GetCurrentUser(string username);
        bool ValidateUser(string username, string password);
        IQueryable<User> GetUserByUsername(string username, User loggedInUser);
        IQueryable<User> GetAllUsers();
        User GetUserByUserId(int id);
        void Save();
        void Delete(User user);
        bool MailAddressExists(string mailAddress);
        bool IsAdmin(string username);
        string renewUserVerificationCode(string mailAddress);
        void SetAuthCookie(string username, bool createPersistentCookie);
        void SignOut();
    }
}
