using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Models;
using System.Security.Principal;
using MovieLibrary.Service.IServices;
using Rhino.Mocks;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Helpers;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using UnitTests;

namespace MovieLibrary.Test.Integration
{
    [TestClass]
    public class MediaRequestsIntTest
    {
        protected static List<InsertRequest> addedObjects = new List<InsertRequest>();
        protected static IQueryable<InsertRequest> insertRequests;
        protected const int pageSize = 10;
        protected static User admin;
        protected static User user;
        protected static IPrincipal principalAdmin;
        protected static IPrincipal principalUser;
        protected static MockRepository mocks = new MockRepository();

        protected static string mediaRequests_ViewName = "MediaRequests";
        protected static string notAuthorized_ViewName = "NotAuthorized";
        protected static string notFound_ViewName = "NotFound";

        protected void AddObjects(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                InsertRequest insertRequest = TestUtil.generateInsertRequest();
                insertRequest.User = context.UserSet.First<User>();
                context.InsertRequestSet.AddObject(insertRequest);
                addedObjects.Add(insertRequest);
            }
            context.SaveChanges();
        }

        protected void DeleteObjects()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (InsertRequest insertRequest in addedObjects)
            {
                context.DeleteObject(insertRequest);
            }
            context.SaveChanges();

            addedObjects.Clear();
        }

        protected InsertRequest CreateInsertRequest(Media media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            InsertRequest insertRequest = new InsertRequest();
            insertRequest.RequestDate = DateTime.Now;
            insertRequest.User = context.UserSet.First<User>();
            insertRequest.Media = media;

            context.InsertRequestSet.AddObject(insertRequest);
            context.SaveChanges();

            return insertRequest;
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
            admin = TestUtil.CreateAdmin();
            user = TestUtil.CreateUser();
            principalAdmin = new GenericPrincipal(new GenericIdentity(admin.Username, "Forms"), null);
            principalUser = new GenericPrincipal(new GenericIdentity(user.Username, "Forms"), null);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DeleteObjects();
        }

        [TestMethod]
        public void MediaRequestsShouldReturnMediaRequestsView()
        {
            AdminController adminController = new AdminController();

            ViewResult result = adminController.MediaRequests(principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void MediaRequestsShouldReturnExpectedResultsPage()
        {
            AddObjects(20);

            AdminController adminController = new AdminController();

            ViewResult result = adminController.MediaRequests(principalAdmin, 1) as ViewResult;
            Assert.IsNotNull(result);

            PaginatedList<InsertRequest> actual = result.ViewData.Model as PaginatedList<InsertRequest>;
            Assert.IsNotNull(actual);

            Assert.AreEqual(pageSize, actual.Count);
            Assert.AreEqual(1, actual.PageIndex);
        }
        
        [TestMethod]
        public void MediaRequestsShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController();

            ViewResult result = adminController.MediaRequests(principalUser, 1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            
            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestBook(insertRequest.Id, formCollection, principalAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, principalUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotFoundView_NoBookFound()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateMovie());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["Isbn"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Isbn"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestBookShouldReturnExpectedViewData_ValidationErrors()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }

        [TestMethod]
        public void ConfirmRequestMovieShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateMovie());

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestMovie(insertRequest.Id, formCollection, principalAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, principalUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotFoundView_NoMovieFound()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateMovie());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";
            formCollection["ReleaseDate"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ReleaseDate"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestMoviekShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateMovie());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["ReleaseDate"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ReleaseDate"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnExpectedViewData_ValidationErrors()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateMovie());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";
            formCollection["ReleaseDate"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestTVShow(insertRequest.Id, formCollection, principalAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, principalUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController();

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotFoundView_NoTVShowFound()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateBook());
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";
            formCollection["Seasons"] = "";
            formCollection["ShowBeginning"] = "";
            formCollection["ShowEnding"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Seasons"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ShowBeginning"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowkShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["Seasons"] = "101";
            formCollection["ShowBeginning"] = "bogus";
            formCollection["ShowEnding"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Seasons"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ShowBeginning"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ShowEnding"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnExpectedViewData_ValidationErrors()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));
            addedObjects.Add(insertRequest);

            mocks.SetFakeControllerContext(adminController);

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";
            formCollection["Seasons"] = "";
            formCollection["ShowBeginning"] = "";
            formCollection["ShowEnding"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(insertRequest.Id, formCollection, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }

        [TestMethod]
        public void DeclineRequestShouldReturnRedirectToAction()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));

            RedirectToRouteResult result = adminController.DeclineRequest(insertRequest.Id, principalAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void DeclineRequestShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController();

            InsertRequest insertRequest = CreateInsertRequest(TestUtil.generateTV_Show(true));
            addedObjects.Add(insertRequest);

            ViewResult result = adminController.DeclineRequest(insertRequest.Id, principalUser) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void DeclineRequestShouldReturnNotFoundView()
        {
            AdminController adminController = new AdminController();

            ViewResult result = adminController.DeclineRequest(7, principalAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
    }
}
