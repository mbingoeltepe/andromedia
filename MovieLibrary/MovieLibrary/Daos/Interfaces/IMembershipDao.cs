using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IMembershipDao
    {
        bool RegisterUser(string mailAddress, string password);
        bool VerifyUser(string mailAddress, string verificationCode);
        bool ChangePassword(string username, string password);
        User GetCurrentUser(string username);
        bool ValidateUser(string username, string password);
        IQueryable<User> GetUserByUsername(string username);
        void Save();
        void Delete(User user);
        bool MailAddressExists(string mailAddress);
        string renewUserVerificationCode(string mailAddress);
    }
}
