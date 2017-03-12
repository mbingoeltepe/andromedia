using System.Web;
using System.Web.Mvc;
using MovieLibrary.Daos.EntityFramework;
using MovieLibrary.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Service.IServices;

namespace MovieLibrary.Controllers
{
    public class LoginController : BaseController
    {       
        private IMembershipService membershipService;

        public LoginController()
        {
            membershipService = MembershipService.Instance;
        }

        public LoginController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        //
        // GET: /Login/
        private static User loggedUser = new User();
        private static int MINPASSWORDLENGTH = 5;
        public ActionResult Index()
        {
            return View("Login");
        }

        public PartialViewResult ShowLogIn()
        {
            return PartialView("ShowLogIn");
        }
        /// <summary>
        /// Loginfunktion, validiert die Userdaten und loggt den user ein
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        ///     
        #if !DEBUG
        [RequireHttps]
        #endif
        public ActionResult Login(FormCollection collection)
        {
            string mailAddress = collection["Username"];
            string password = collection["Password"];
            string i = collection["RememberCheckBox"];
            bool setCookie = collection["RememberCheckBox"].Contains("true");
            if (MembershipDao.Instance.ValidateUser(mailAddress, password))
            {
                User user = MembershipDao.Instance.GetCurrentUser(mailAddress);
                loggedUser = user;
                membershipService.SetAuthCookie(user.Username, setCookie);
                //FormsAuthentication.SetAuthCookie(user.Username, setCookie);
                return RedirectToAction("ShowProfile", "User");
                // return Redirect("../");
            }
            else
            {
                return View("LoginError");
            }
        }

        #if !DEBUG
        [RequireHttps]
        #endif
        public ActionResult SecureLogin()
        {
            return View("Login");
        }

        #if !DEBUG
        [RequireHttps]
        #endif
        public ActionResult SecureRegister()
        {
            return View("RegisterForm");
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            membershipService.SignOut();
            //FormsAuthentication.SignOut();
            return Redirect("../");
        }

        public PartialViewResult ShowRegister()
        {
            return PartialView();        
        }

        #if !DEBUG
        [RequireHttps]
        #endif
        public ActionResult SendVerificationEMail(FormCollection collection)
        {
            string mailAddress = collection["TextBoxEMail"];
            if (collection["TextBoxPassword"].Equals(collection["TextBoxPasswordConfirm"]) && collection["TextBoxPassword"].Length >= MINPASSWORDLENGTH && mailAddress.Contains("@"))
            {
                try
                {
                    string password = collection["TextBoxPassword"];
                    if (!MembershipService.Instance.MailAddressExists(mailAddress))
                    {
                        //UrlHelper helper = new UrlHelper(this.ControllerContext.RequestContext);
                        //string url = helper.Action("VerifyRegistration", "Login", new { username = mailAddress });

                        MembershipService.Instance.RegisterUser(mailAddress, password);

                        User user = MembershipService.Instance.GetCurrentUser(mailAddress);

                        string link;
                        //if (HttpContext.Request.Url.IsDefaultPort)
                        {
                            link = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, Url.Action("VerifyRegistration", "Login", new { username = mailAddress, verificationCode = user.VerificationCode }));
                        }
                        //else
                        //{
                        //    link = string.Format("{0}://{1}:{2}{3}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, HttpContext.Request.Url.Port, Url.Action("VerifyRegistration", "Login", new { username = mailAddress, verificationCode = user.VerificationCode }));
                        //}


                        MailMessage msg = new MailMessage();
                        MailAddress to = new MailAddress(mailAddress);
                        MailAddress from = new MailAddress("andromedia@gmx.at");

                        msg.To.Add(to);
                        msg.From = from;
                        msg.Subject = "Andromedia Registrierung";
                        msg.IsBodyHtml = true;
                        msg.Body = "Bestätigen Sie Ihre Registrierung durch Klicken des folgenden Links: <br /> <a href='" + link + "'>" + link + "</a>";

                        SmtpClient smtp = new SmtpClient("mail.gmx.net");

                        smtp.Credentials = new NetworkCredential("andromedia@gmx.at", "andro20media11");

                        try
                        {
                            smtp.Send(msg);
                        }
                        catch (Exception)
                        {
                            MembershipService.Instance.Delete(MembershipService.Instance.GetCurrentUser(mailAddress));
                            ViewData["errorMessage"] = "Beim Senden des Links ist ein Fehler aufgetreten";
                            return View("RegisterError");
                        }

                        ViewData["link"] = link;
                        return View("Register");
                    }
                }

                catch (Exception)
                {
                    MembershipService.Instance.Delete(MembershipService.Instance.GetCurrentUser(mailAddress));
                    return View("RegisterError");
                }
            }
            else
            {
                MembershipService.Instance.Delete(MembershipService.Instance.GetCurrentUser(mailAddress));
            }
            return View("RegisterError");
        }

        /*
        #if !DEBUG
        [RequireHttps]
        #endif
        public ActionResult Register(FormCollection collection)
        {
            string mailAddress = collection["TextBoxEMail"];
            if (collection["TextBoxPassword"].Equals(collection["TextBoxPasswordConfirm"]))
            {
                string password = collection["TextBoxPassword"];
                if (MembershipDao.Instance.RegisterUser(mailAddress, password))
                {
                    //UrlHelper helper = new UrlHelper(this.ControllerContext.RequestContext);
                    //string url = helper.Action("VerifyRegistration", "Login", new { username = mailAddress });
                    string link = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, Url.Action("VerifyRegistration", "Login", new { username = mailAddress }));

                    ViewData["link"] = link;
                    return View("Register");
                }

            }
            return View();
        }
        */

        public ActionResult VerifyRegistration(string username, string verificationCode)
        {
            if (membershipService.VerifyUser(username, verificationCode))
            {
                User user = MembershipDao.Instance.GetCurrentUser(username);
                loggedUser = user;
                membershipService.SetAuthCookie(user.Username, false);
                return RedirectToAction("ViewVerificationMessage", "User");
            }
            else return View("../../Views/Error/VerificationError");
        }

        public ActionResult ForgottenPassword()
        {
            return View("ForgottenPassword");
        }

        public ActionResult SendForgottenPasswordLink(FormCollection collection)
        {
            string mailAddress = collection["TextBoxEMail"];

            if (MembershipService.Instance.MailAddressExists(mailAddress))
            {
                try
                {
                    User user = MembershipService.Instance.GetCurrentUser(mailAddress);
                    MembershipService.Instance.renewUserVerificationCode(mailAddress);

                    string link;
                    //if (HttpContext.Request.Url.IsDefaultPort)
                    {
                        link = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, Url.Action("ResetPassword", "Login", new { username = mailAddress, verificationCode = user.VerificationCode }));
                    }
                    //else
                    //{
                    //   link = string.Format("{0}://{1}:{2}{3}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, HttpContext.Request.Url.Port, Url.Action("ResetPassword", "Login", new { username = mailAddress, verificationCode = user.VerificationCode }));
                    //}

                    MailMessage msg = new MailMessage();
                    MailAddress to = new MailAddress(mailAddress);
                    MailAddress from = new MailAddress("andromedia@gmx.at");

                    msg.To.Add(to);
                    msg.From = from;
                    msg.Subject = "Andromedia Passwort zurücksetzen";
                    msg.IsBodyHtml = true;
                    msg.Body = "Klicken Sie den folgenden Link, um Ihr Passwort zurückzusetzen: <br /> <a href='" + link + "'>" + link + "</a>";

                    SmtpClient smtp = new SmtpClient("mail.gmx.net");

                    smtp.Credentials = new NetworkCredential("andromedia@gmx.at", "andro20media11");

                    try
                    {
                        smtp.Send(msg);

                        ViewData["link"] = link;
                        ViewData["title"] = "Passwort zurücksetzen - Andromedia";
                        ViewData["message"] = "Eine E-Mail mit weiteren Anweisungen wurde Ihnen auf Ihre angegebene Adresse zugesandt.";
                        return View("LoginMessage");
                    }
                    catch (Exception)
                    {
                        ViewData["title"] = "Fehler - Andromedia";
                        ViewData["errorMessage"] = "Beim Senden des Links ist ein Fehler aufgetreten";
                        return View("LoginError");
                    }
                }

                catch (Exception e)
                {
                    ViewData["title"] = "Fehler - Andromedia";
                    ViewData["errorMessage"] = e.Message;
                    return View("LoginError");
                }
            }
            else
            {
                ViewData["title"] = "Fehler - Andromedia";
                ViewData["errorMessage"] = "Diese E-Mail Adresse ist bei uns nicht registriert";
                return View("LoginError");
            }
        }

#if !DEBUG
        [RequireHttps]
#endif
        [Authorize]
        public ActionResult ResetPasswordNow(string username)
        {
            ViewData["mailAddress"] = username;
            return View("ResetPasswordForm");
        }

#if !DEBUG
        [RequireHttps]
#endif
        public ActionResult ResetPassword(string username, string verificationCode)
        {
            if (MembershipService.Instance.VerifyUser(username, verificationCode))
            {
                ViewData["mailAddress"] = username;
                return View("ResetPasswordForm");
            }
            else return View("../../Views/Error/VerificationError");
        }

#if !DEBUG
        [RequireHttps]
#endif
        public ActionResult DoResetPassword(FormCollection collection)
        {
            string mailAddress = collection["TextBoxEMail"];
            string password = collection["TextBoxPassword"];
            string passwordConfirm = collection["TextBoxPasswordConfirm"];

            if (collection["TextBoxPassword"].Equals(collection["TextBoxPasswordConfirm"])
                && collection["TextBoxPassword"].Length >= MINPASSWORDLENGTH && mailAddress.Contains("@")
                && MembershipService.Instance.MailAddressExists(mailAddress))
            {
                if (MembershipService.Instance.ChangePassword(mailAddress, password))
                {
                    MembershipService.Instance.renewUserVerificationCode(mailAddress);

                    ViewData["title"] = "Passwort geändert - Andromedia";
                    ViewData["heading"] = "Passwort geändert";
                    ViewData["message"] = "Ihr Passwort wurde erfolgreich geändert";
                    
                    return View("LoginMessage");
                }
            }
            else if (!MembershipService.Instance.MailAddressExists(mailAddress))
            {
                ViewData["errorMessage"] = "Die angegebene E-Mail Adresse ist bei uns nicht registriert.";
                return View("LoginError");
            }
            else if (!password.Equals(passwordConfirm))
            {
                ViewData["errorMessage"] = "Die angegebenen Passwörter stimmen nicht überein.";
                return View("LoginError");
            }
            else if (password.Length < MINPASSWORDLENGTH)
            {
                ViewData["errorMessage"] = "Das angegebene Passwort muss mindestens " + MINPASSWORDLENGTH + " Zeichen lang sein.";
                return View("LoginError");
            }
            ViewData["errorMessage"] = "Es ist ein unerwarteter Fehler aufgetreten =/";
            return View("LoginError");
        }
    }
}
