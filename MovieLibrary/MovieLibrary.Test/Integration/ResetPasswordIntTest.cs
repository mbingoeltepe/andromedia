using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Controllers;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using UnitTests;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class ResetPasswordIntTest
    {
        protected static User user;

        protected static string loginMessage_ViewName = "LoginMessage";
        protected static string loginError_ViewName = "LoginError";
        protected static string resetPasswordForm_ViewName = "ResetPasswordForm";
        protected static string verificationError_ViewName = "../../Views/Error/VerificationError";

        protected static MockRepository mocks = new MockRepository();

        protected static void SaveUserToDb(User user)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.UserSet.AddObject(user);
            context.SaveChanges();
        }

        protected static void DeleteUsers()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (User user in context.UserSet)
            {
                context.UserSet.DeleteObject(user);
            }
            context.SaveChanges();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();

            user = new User();
            user.Username = "andromedia@gmx.at";
            user.Password = "test1";
            user.Verified = true;
            user.VerificationCode = "0123456789";

            SaveUserToDb(user);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUsers();
        }
        
        [TestMethod]
        public void SendForgottenPasswordLinkShouldReturnLoginMessageView()
        {
            LoginController loginController = new LoginController();

            HttpContextBase httpContext = mocks.FakeHttpContext();
            RequestContext requestContext = new RequestContext(httpContext, new RouteData());
            ControllerContext controllerContext = new ControllerContext(requestContext, loginController);

            loginController.ControllerContext = controllerContext;
            loginController.Url = new UrlHelper(requestContext);

            Uri uri = new Uri(@"http://www.andromedia.com/");
            SetupResult.For(httpContext.Request.Url).Return(uri);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = user.Username;

            ViewResult result = loginController.SendForgottenPasswordLink(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginMessage_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["link"]);
            Assert.IsNotNull(result.ViewData["title"]);
            Assert.IsNotNull(result.ViewData["message"]);
        }

        [TestMethod]
        public void SendForgottenPasswordLinkShouldReturnLoginErrorView_NoSuchEmail()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = "bogus@test.com";

            ViewResult result = loginController.SendForgottenPasswordLink(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["title"]);
            Assert.IsNotNull(result.ViewData["errorMessage"]);
        }


        [TestMethod]
        public void ResetPasswordShouldReturnResetPasswordFormView()
        {
            LoginController loginController = new LoginController();

            ViewResult result = loginController.ResetPassword(user.Username, user.VerificationCode) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(resetPasswordForm_ViewName, result.ViewName);
            Assert.AreEqual(user.Username, result.ViewData["mailAddress"]);
        }

        [TestMethod]
        public void ResetPasswordShouldReturnResetPasswordFormView_FalseVerificationCode()
        {
            LoginController loginController = new LoginController();

            ViewResult result = loginController.ResetPassword(user.Username, "9876543210") as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(verificationError_ViewName, result.ViewName);
        }

        [TestMethod]
        public void DoResetPasswordShouldReturnLoginMessageView()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = user.Username;
            formCollection["TextBoxPassword"] = "new_pwd";
            formCollection["TextBoxPasswordConfirm"] = "new_pwd";

            ViewResult result = loginController.DoResetPassword(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginMessage_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["title"]);
            Assert.IsNotNull(result.ViewData["heading"]);
            Assert.IsNotNull(result.ViewData["message"]);
        }

        [TestMethod]
        public void DoResetPasswordShouldReturnLoginErrorView_NoSuchEmail()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = "bogus@test.com";
            formCollection["TextBoxPassword"] = "new_pwd";
            formCollection["TextBoxPasswordConfirm"] = "new_pwd";

            ViewResult result = loginController.DoResetPassword(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["errorMessage"]);
        }

        [TestMethod]
        public void DoResetPasswordShouldReturnLoginErrorView_PasswordConfirmError()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = user.Username;
            formCollection["TextBoxPassword"] = "pwd";
            formCollection["TextBoxPasswordConfirm"] = "new_pwd";

            ViewResult result = loginController.DoResetPassword(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["errorMessage"]);
        }

        [TestMethod]
        public void DoResetPasswordShouldReturnLoginErrorView_PasswordToShort()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = user.Username;
            formCollection["TextBoxPassword"] = "pwd";
            formCollection["TextBoxPasswordConfirm"] = "pwd";

            ViewResult result = loginController.DoResetPassword(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["errorMessage"]);
        }

        [TestMethod]
        public void DoResetPasswordShouldReturnLoginErrorView_EmailNotValid()
        {
            LoginController loginController = new LoginController();

            FormCollection formCollection = new FormCollection();
            formCollection["TextBoxEMail"] = "bogus";
            formCollection["TextBoxPassword"] = "new_pwd";
            formCollection["TextBoxPasswordConfirm"] = "new_pwd";

            ViewResult result = loginController.DoResetPassword(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["errorMessage"]);
        }
    }
}
