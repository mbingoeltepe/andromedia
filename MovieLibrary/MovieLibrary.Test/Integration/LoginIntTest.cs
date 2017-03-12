using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using UnitTests;
using Rhino.Mocks;
using System.Web.Routing;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using System.Web.Security;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using System.Security.Cryptography;
using System.Security.Principal;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class LoginIntTest
    {
        protected static string profile_ViewName = "ShowProfile";
        protected static string loginError_ViewName = "LoginError";
        protected static string root_ViewName = "../";

        protected static MockRepository mocks = new MockRepository();

        protected static User testUser = new User();
        protected static IPrincipal fakeUser;

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

            testUser.Username = "andromedia@gmx.at";
            testUser.Password = MovieLibrary.Daos.EntityFramework.MembershipDao.HashSHA1("password");
            testUser.Verified = true;

            SaveUserToDb(testUser);

            fakeUser = new GenericPrincipal(new GenericIdentity(testUser.Username, "Forms"), null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUsers();
        }

        [TestMethod]
        public void LoginShouldReturnShowProfile()
        {
            IMembershipService membershipService = mocks.StrictMock<IMembershipService>();
            LoginController loginController = new LoginController(membershipService);

            HttpContextBase httpContext = mocks.FakeHttpContext();
            RequestContext requestContext = new RequestContext(httpContext, new RouteData());
            ControllerContext controllerContext = new ControllerContext(requestContext, loginController);

            loginController.ControllerContext = controllerContext;
            loginController.Url = new UrlHelper(requestContext);

            Expect.Call(delegate { membershipService.SetAuthCookie(testUser.Username, false); });
            mocks.ReplayAll();

            // FormsAuthentication.Authenticate(testUser.Username, testUser.Password);

            FormCollection formCollection = new FormCollection();
            formCollection["Username"] = "andromedia@gmx.at";
            formCollection["Password"] = "password";
            formCollection["RememberCheckBox"] = "false";

            RedirectToRouteResult result = loginController.Login(formCollection) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(profile_ViewName, result.RouteValues["action"]);
            Assert.AreEqual("User", result.RouteValues["controller"]);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void LoginShouldReturnLoginError()
        {
            LoginController loginController = new LoginController();

            HttpContextBase httpContext = mocks.FakeHttpContext();
            RequestContext requestContext = new RequestContext(httpContext, new RouteData());
            ControllerContext controllerContext = new ControllerContext(requestContext, loginController);

            loginController.ControllerContext = controllerContext;
            loginController.Url = new UrlHelper(requestContext);

            mocks.ReplayAll();

            // FormsAuthentication.Authenticate(testUser.Username, testUser.Password);

            FormCollection formCollection = new FormCollection();
            formCollection["Username"] = "andromedia@gmx.at";
            formCollection["Password"] = "wrong";
            formCollection["RememberCheckBox"] = "false";

            ViewResult result = loginController.Login(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void LogoutShouldReturnRoot()
        {
            IMembershipService membershipService = mocks.StrictMock<IMembershipService>();
            LoginController loginController = new LoginController(membershipService);

            HttpContextBase httpContext = mocks.FakeHttpContext();
            RequestContext requestContext = new RequestContext(httpContext, new RouteData());
            ControllerContext controllerContext = new ControllerContext(requestContext, loginController);

            loginController.ControllerContext = controllerContext;
            loginController.Url = new UrlHelper(requestContext);

            Expect.Call(delegate { membershipService.SignOut(); });
            Expect.Call(delegate { httpContext.Session.Clear(); });
            mocks.ReplayAll();

            // mocks.SetFakeControllerContext(loginController);

            // FormsAuthentication.Authenticate(testUser.Username, testUser.Password);

            FormCollection formCollection = new FormCollection();
            formCollection["Username"] = "andromedia@gmx.at";
            formCollection["Password"] = "wrong";
            formCollection["RememberCheckBox"] = "false";

            ViewResult result = loginController.Login(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(loginError_ViewName, result.ViewName);

            RedirectResult logoutResult = loginController.LogOff() as RedirectResult;

            Assert.IsNotNull(logoutResult);
            Assert.AreEqual(root_ViewName, logoutResult.Url);
            mocks.VerifyAll();
        }

    }
}
