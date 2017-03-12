using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;
using System.Web.UI;
using System.Security.Principal;
using MovieLibrary.Service.ServicesImpl;

namespace MovieLibrary.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ShowProfile(IPrincipal user)
        {
            List<UserMedia> userMedia = UserMediaDaoEF.Instance.GetByUser(user.Identity.Name).ToList();
            User loggedInUser = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            //UserMediaDaoEF.Instance.GetRecommendedMedia(user.Identity.Name);
            return View("ShowProfile", loggedInUser);
        }
        
        

        [Authorize]
        public ActionResult ViewVerificationMessage(IPrincipal user)
        {
            return View("../Login/VerifyRegistration");
        }

        [Authorize]
        public ActionResult ShowFriends(IPrincipal user)
        {
            User onlineUser = MembershipService.Instance.GetCurrentUser(user.Identity.Name);

            return View(onlineUser);
        }

        [Authorize]
        public ActionResult ShowUserMediathek(string username, IPrincipal user)
        {
            User loggedInUser = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            if (username != null)
            {
                User friend = MembershipService.Instance.GetCurrentUser(username);
                if (loggedInUser.Friends.Contains(friend) || loggedInUser == friend)
                {
                    return View("ShowMediathek", friend);
                }
            }
            else
            {
                return View("ShowMediathek", loggedInUser);
            }
            return null;
        }

        [Authorize]
        public ActionResult SearchUser(FormCollection collection, IPrincipal user)
        {
            string username = collection["UsernameBox"];
            IQueryable<User> userFriend = MembershipService.Instance.GetUserByUsername(username, MembershipService.Instance.GetCurrentUser(user.Identity.Name));
            return View(userFriend);
        }

        [Authorize]
        public ActionResult SendFriendRequest(string username, IPrincipal user)
        {
            User onlineUser = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            User userFriend = MembershipService.Instance.GetCurrentUser(username);
            if (!onlineUser.Username.Equals(userFriend.Username) && !onlineUser.Friends.Contains(userFriend))
            {
                userFriend.Requestlist.Add(onlineUser);
            }

            MembershipService.Instance.Save();

            ViewData["title"] = "Freund Anfrage - Andromedia";
            ViewData["message"] = "Der User '" + username + "' wurde über die Anfrage benachrichtigt";
            ViewData["link"] = "ShowFriends";
            ViewData["linkText"] = "Zurück";
            return View("Message");
            // return RedirectToAction("ShowFriends"); // @TODO: DOES NOT REDIRECT AT ALL
        }

        [Authorize]
        public ActionResult ConfirmFriendRequest(string username, IPrincipal user)
        {
            User userFriend = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            User onlineUser = MembershipService.Instance.GetCurrentUser(username);
            userFriend.Requestlist.Remove(onlineUser);
            userFriend.Friends.Add(onlineUser);
            onlineUser.Friends.Add(userFriend);
            MembershipService.Instance.Save();

            return RedirectToAction("ShowFriends");
        }

        [Authorize]
        public ActionResult DiscardFriendRequest(string username, IPrincipal user)
        {
            User userFriend = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            User onlineUser = MembershipService.Instance.GetCurrentUser(username);
            userFriend.Requestlist.Remove(onlineUser);
            MembershipService.Instance.Save();

            return RedirectToAction("ShowFriends");
        }

        [Authorize]
        public ActionResult RemoveFriend(string username, IPrincipal user)
        {
            User userFriend = MembershipService.Instance.GetCurrentUser(user.Identity.Name);
            User onlineUser = MembershipService.Instance.GetCurrentUser(username);
            userFriend.Friends.Remove(onlineUser);
            onlineUser.Friends.Remove(userFriend);
            MembershipService.Instance.Save();

            return RedirectToAction("ShowFriends");
        }
    }
}
