using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Daos.EntityFramework;
using System.Web.Security;
using MovieLibrary.Models;

namespace MovieLibrary.Service.ServicesImpl
{
    public class MembershipService : IMembershipService
    {
        private static readonly MembershipService instance = new MembershipService();

        private MembershipService()
        {
        }

        public static MembershipService Instance
        {
            get 
            {
                return instance;
            }
        }

        #region IMemebershipService Members

        public bool RegisterUser(string mailAddress, string password)
        {
            return MembershipDao.Instance.RegisterUser(mailAddress, password);
        }

        public bool VerifyUser(string mailAddress, string verificationCode)
        {
            return MembershipDao.Instance.VerifyUser(mailAddress, verificationCode);
        }

        public bool ChangePassword(string username, string password)
        {
            return MembershipDao.Instance.ChangePassword(username, password);
        }

        public Models.User GetCurrentUser(string username)
        {
            return MembershipDao.Instance.GetCurrentUser(username);
        }

        public bool ValidateUser(string username, string password)
        {
            return MembershipDao.Instance.ValidateUser(username, password);
        }

        #endregion

        #region IMembershipService Members


        public void Save()
        {
            MembershipDao.Instance.Save();
        }

        public void Delete(Models.User user)
        {
            MembershipDao.Instance.Delete(user);
        }

        #endregion


        public IQueryable<Models.User> GetUserByUsername(string username, User loggedInUser)
        {
            List<User> user = new List<User>();
            var result = MembershipDao.Instance.GetUserByUsername(username);
            foreach(var r in result)
            {
                if(!loggedInUser.Friends.Contains(r) && loggedInUser.Id != r.Id)
                {
                    user.Add(r);
                }
            }
            return user.AsQueryable<User>();
        }

        public Models.User GetUserByUserId(int id)
        {
            return MembershipDao.Instance.GetUserByUserId(id);
        }

        public IQueryable<Models.User> GetAllUsers()
        {
            return MembershipDao.Instance.GetAllUsers();
        }

        public bool MailAddressExists(string mailAddress)
        {
            return MembershipDao.Instance.MailAddressExists(mailAddress);
        }

        public bool IsAdmin(string username)
        {
            return MembershipDao.Instance.IsAdmin(username);
        }

        public string renewUserVerificationCode(string mailAddress)
        {
            return MembershipDao.Instance.renewUserVerificationCode(mailAddress);
        }

        public void SetAuthCookie(string username, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}