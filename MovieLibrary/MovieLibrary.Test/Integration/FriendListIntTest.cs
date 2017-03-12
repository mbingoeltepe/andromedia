using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Models;
using Rhino.Mocks;
using UnitTests;
using MovieLibrary.Helpers;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class FriendListIntTest
    {
        protected static User user;
        protected static User friendUser;
        protected static IPrincipal fakeUser;

        protected static MockRepository mocks = new MockRepository();

        protected static void CreateTestContext()
        {
            using (MediaLibContainer context = new MediaLibContainer())
            {
                user = new User();
                user.Username = "test@test.com";
                user.Password = "test";
                user.Verified = true;
                user.Closed = false;

                friendUser= new User();
                friendUser.Username = "other@test.com";
                friendUser.Password = "other";
                friendUser.Verified = true;
                friendUser.Closed = false;


                context.UserSet.AddObject(user);
                context.UserSet.AddObject(friendUser);
                context.SaveChanges();
            }

            fakeUser = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null);
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

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();

        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();
            CreateTestContext();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteUsers();
        }


        [TestMethod()]
        public void TestViewName()
        {
            var controller = new UserController();
            var result = controller.ShowFriends(fakeUser) as ViewResult;

            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod()]
        public void TestSearchUser()
        {

            FormCollection collection = new FormCollection();
            collection["UsernameBox"] = "other@test.com";

            var controller = new UserController();


            var result = controller.SearchUser(collection, fakeUser) as ViewResult;
            var produkt = (IQueryable<User>)result.ViewData.Model;

            Assert.AreEqual(produkt.Count(), 1);
            Assert.AreEqual(produkt.Single().Username, collection["UsernameBox"]);
        }

        [TestMethod()]
        public void TestSendFriendRequest()
        {

            UserController controller = new UserController();
            HttpContextBase httpContext = mocks.FakeHttpContext();

            var context = MockRepository.GenerateMock<HttpContextBase>();
            var request = MockRepository.GenerateMock<HttpRequestBase>();
            request.Expect(r => r.UrlReferrer).Return(new Uri("http://www.andromedia.com")).Repeat.AtLeastOnce();
            context.Expect(c => c.Request).Return(request).Repeat.Any();

            httpContext = context;

            RequestContext requestContext = new RequestContext(httpContext, new RouteData());            
            ControllerContext controllerContext = new ControllerContext(requestContext, controller);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(requestContext);
            
            mocks.ReplayAll();

            FormCollection collection = new FormCollection();
            collection["UsernameBox"] = "other@test.com";           


            var resultSearch = controller.SearchUser(collection, fakeUser) as ViewResult;
            var produkt = (IQueryable<User>)resultSearch.ViewData.Model;

            User userFriend = new User();

            userFriend = produkt.Single();

            var result = controller.SendFriendRequest(userFriend.Username , fakeUser) as ViewResult;

            Assert.AreEqual(userFriend.Requestlist.Single().Username ,user.Username);
           

        }

        [TestMethod()]
        public void TestConfirmFriendRequest()
        {

            UserController controller = new UserController();
            HttpContextBase httpContext = mocks.FakeHttpContext();

            var context = MockRepository.GenerateMock<HttpContextBase>();
            var request = MockRepository.GenerateMock<HttpRequestBase>();
            request.Expect(r => r.UrlReferrer).Return(new Uri("http://www.andromedia.com")).Repeat.AtLeastOnce();
            context.Expect(c => c.Request).Return(request).Repeat.Any();

            httpContext = context;

            RequestContext requestContext = new RequestContext(httpContext, new RouteData());
            ControllerContext controllerContext = new ControllerContext(requestContext, controller);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(requestContext);

            mocks.ReplayAll();

            FormCollection collection = new FormCollection();
            collection["UsernameBox"] = "other@test.com";


            var resultSearch = controller.SearchUser(collection, fakeUser) as ViewResult;
            var produkt = (IQueryable<User>)resultSearch.ViewData.Model;

            User userFriend = new User();

            userFriend = produkt.Single();

            var resultSend = controller.SendFriendRequest(userFriend.Username, fakeUser) as ViewResult;
            
            var resultConfirm = controller.ConfirmFriendRequest(userFriend.Username, fakeUser) as ViewResult;

            Assert.AreEqual(userFriend.Friends.Single().Username, user.Username);


        }



        
    }
}
