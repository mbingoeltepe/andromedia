using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using System.Security.Cryptography;
using System.Text;
using MovieLibrary.Daos.Interfaces;

namespace MovieLibrary.Daos.EntityFramework
{
    public class MembershipDao:IMembershipDao
    {
        private static readonly MembershipDao instance = new MembershipDao();

        private MembershipDao()
        {
        }

        public static MembershipDao Instance
        {
            get
            {
                return instance;
            }
        }

        public bool MailAddressExists(string mailAddress)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var result = from i in context.UserSet
                         where i.Username == mailAddress
                         select i;
            if (result.Count() > 0)
                return true;
            return false;
        }

        public bool RegisterUser(string mailAddress, string password)
        {
            User user = new User();
            user.Username = mailAddress;
            user.Password = HashSHA1(password);
            renewUserVerificationCode(user);
            user.Verified = false;
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
            try
            {
                context.AddToUserSet(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string renewUserVerificationCode(string mailAddress)
        {
            User user = GetCurrentUser(mailAddress);
            if (user != null)
            {
                return renewUserVerificationCode(user);
            }
            else return "";
        }

        private string renewUserVerificationCode(User user)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            user.VerificationCode = rand.Next(1000000, 10000000).ToString() + rand.Next(1000000, 10000000).ToString() +
                rand.Next(1000000, 10000000).ToString() + rand.Next(1000000, 10000000).ToString() +
                rand.Next(1000000, 10000000).ToString() + rand.Next(1000000, 10000000).ToString();

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                context.SaveChanges();
                return user.VerificationCode;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public User GetCurrentUser(string username)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            try
            {
                return (from i in context.UserSet
                        where username == i.Username                        
                        select i).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool ValidateUser(string username, string password)
        {
            string pw = HashSHA1(password);
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            var user = from i in context.UserSet
                       where i.Username == username
                       select i;

            User validatedUser = new User();

            if (user.Count() == 1)
            {
                validatedUser = user.Single();
            }

            if (pw == validatedUser.Password && validatedUser.Verified && validatedUser.Closed == false)
            {
                return true;
            }

            return false;
        }

        public bool ChangePassword(string username, string password)
        {
            if (MailAddressExists(username))
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                GetCurrentUser(username).Password = HashSHA1(password);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        #region passwordHash


        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPwd, "sha1");
            return hashedPwd;
        }

        public static string HashSHA1(string input)
        {
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            //Salt hinzufügen
            input += "s.s21x-s21";
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            byte[] result = provider.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString());
            }
            return sb.ToString();
        }

        #endregion



        #region IMembershipDao Members


        public bool VerifyUser(string mailAddress, string verificationCode)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var user = (from i in context.UserSet
                        where i.Username == mailAddress
                        select i).Single();
            if (user.VerificationCode.Equals(verificationCode))
            {
                user.Verified = true;
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else return false;
        }

        #endregion

        #region IMembershipDao Members


        public void Save()
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                context.SaveChanges();
            }
            catch (Exception)
            { }
        }

        public void Delete(User user)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

                context.UserSet.DeleteObject(user);
                context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        #endregion


        public IQueryable<User> GetUserByUsername(string username)
        {
            if (username == null) username = "";
            List<User> names = new List<User>();
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var result = from user in context.UserSet
                         where user.Username.Contains(username) &&
                         user.Verified == true &&
                         user.Closed == false
                         select user;
            if (username.Contains("@"))
            {
                foreach (var r in result)
                {
                    if (!IsAdmin(r.Username))
                        names.Add(r);
                }
                return names.AsQueryable<User>();
            }
            else
            {
                foreach (var u in result)
                {
                    string[] user = u.Username.Split('@');
                    if (user[0].Contains(username)/* && !IsAdmin(u.Username)*/)
                        names.Add(u);
                }
                return names.AsQueryable<User>();
            }
        }

        public User GetUserByUserId(int id)
        {

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var result = (from user in context.UserSet
                          where user.Id == id &&
                          user.Verified == true &&
                          user.Closed == false
                          select user).Single();

            return result;
        }


        public IQueryable<User> GetAllUsers()
        {
            List<User> names = new List<User>();
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            var result = from user in context.UserSet
                         where user.Verified == true
                         where user.Closed == false
                         where !user.Username.Equals("admin@andromedia.com")
                         select user;

            foreach (var u in result)
            {
                names.Add(u);
            }
            return names.AsQueryable<User>();
        }

        public bool IsAdmin(string username)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                (from admin in context.UserSet.OfType<Admin>()
                 where username == admin.Username
                 select admin).Single();
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return true;
        }
    }
}