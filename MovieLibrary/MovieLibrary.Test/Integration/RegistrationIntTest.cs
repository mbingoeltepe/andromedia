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

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class RegistrationIntTest
    {
        protected static string register_ViewName = "Register";
        protected static string registerError_ViewName = "RegisterError";
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUsers();
        }

        [TestMethod]
        public void SendVerficationMailShouldReturnRegisterView()
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
            formCollection["TextBoxEMail"] = "andromedia@gmx.at";
            formCollection["TextBoxPassword"] = "test1";
            formCollection["TextBoxPasswordConfirm"] = "test1";

            ViewResult result = loginController.SendVerificationEMail(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(register_ViewName, result.ViewName);
            Assert.IsNotNull(result.ViewData["link"]);
        }

        [TestMethod]
        public void SendVerficationMailShouldReturnRegisterError_PasswordConfirmError()
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
            formCollection["TextBoxEMail"] = "andromedia@gmx.at";
            formCollection["TextBoxPassword"] = "test1";
            formCollection["TextBoxPasswordConfirm"] = "test0";

            ViewResult result = loginController.SendVerificationEMail(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(registerError_ViewName, result.ViewName);
        }

        [TestMethod]
        public void SendVerficationMailShouldReturnRegisterError_PasswordToShort()
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
            formCollection["TextBoxEMail"] = "andromedia@gmx.at";
            formCollection["TextBoxPassword"] = "test";
            formCollection["TextBoxPasswordConfirm"] = "test";

            ViewResult result = loginController.SendVerificationEMail(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(registerError_ViewName, result.ViewName);
        }

        [TestMethod]
        public void SendVerficationMailShouldReturnRegisterError_FalseEmail()
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
            formCollection["TextBoxEMail"] = "bogus";
            formCollection["TextBoxPassword"] = "test1";
            formCollection["TextBoxPasswordConfirm"] = "test1";

            ViewResult result = loginController.SendVerificationEMail(formCollection) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(registerError_ViewName, result.ViewName);
        }

        [TestMethod]
        public void VerifyRegistrationShouldReturnRedirectToResult()
        {
            User user = new User();
            user.Username = "test@test.com";
            user.Password = "test1";
            user.Verified = false;
            user.VerificationCode = "0123456789";

            SaveUserToDb(user);

            IMembershipService membershipService = mocks.StrictMock<IMembershipService>();
            Expect.Call(membershipService.VerifyUser(user.Username, user.VerificationCode)).Return(true);
            Expect.Call(delegate { membershipService.SetAuthCookie(user.Username, false); });
            mocks.ReplayAll();

            LoginController loginController = new LoginController(membershipService);
            RedirectToRouteResult result = loginController.VerifyRegistration(user.Username, user.VerificationCode) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ViewVerificationMessage", result.RouteValues["action"]);
            Assert.AreEqual("User", result.RouteValues["controller"]);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void VerifyRegistrationShouldReturnVerificationErrorView()
        {
            User user = new User();
            user.Username = "test@test.com";
            user.Password = "test1";
            user.Verified = false;
            user.VerificationCode = "0123456789";

            string falseVerificationCode = "9876543210";

            IMembershipService membershipService = mocks.StrictMock<IMembershipService>();
            Expect.Call(membershipService.VerifyUser(user.Username, falseVerificationCode)).Return(false);
            mocks.ReplayAll();

            LoginController loginController = new LoginController(membershipService);
            ViewResult result = loginController.VerifyRegistration(user.Username, falseVerificationCode) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(verificationError_ViewName, result.ViewName);
            mocks.VerifyAll();
        }
    }
}
