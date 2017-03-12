using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System.Security.Principal;
using MovieLibrary.Helpers;
using UnitTests;

namespace MovieLibrary.Test.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        protected static IQueryable<InsertRequest> insertRequests;
        protected const int pageSize = 10;
        protected static User admin;
        protected static IPrincipal fakeAdmin;
        protected static IMediaService mediaService;
        protected static IInsertRequestService insertRequestService;
        protected static IMembershipService membershipService;
        protected static MockRepository mocks = new MockRepository();

        protected static string mediaRequests_ViewName = "MediaRequests";
        protected static string notAuthorized_ViewName = "NotAuthorized";
        protected static string notFound_ViewName = "NotFound";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            List<InsertRequest> insertRequest_list = new List<InsertRequest>();

            for (int i = 0; i < pageSize*2; i++)
            {
                insertRequest_list.Add(TestUtil.generateInsertRequest());
            }

            insertRequests = insertRequest_list.AsQueryable<InsertRequest>();

            admin = new User();
            admin.Username = "admin@andromedia.com";

            fakeAdmin = new GenericPrincipal(new GenericIdentity(admin.Username, "Forms"), null); 
        }

        [TestInitialize]
        public void TestInitialize()
        {
            mediaService = mocks.StrictMock<IMediaService>();
            insertRequestService = mocks.StrictMock<IInsertRequestService>();
            membershipService = mocks.StrictMock<IMembershipService>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mocks.VerifyAll();
        }

        [TestMethod]
        public void MediaRequestsShouldReturnMediaRequestsView()
        {
            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetAllOrderedByRequestDate()).Return(insertRequests);
            mocks.ReplayAll();

            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            ViewResult result = adminController.MediaRequests(fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
        }

        [TestMethod]
        public void MediaRequestsShouldReturnExpectedResultsPage()
        {
            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetAllOrderedByRequestDate()).Return(insertRequests);
            mocks.ReplayAll();

            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            ViewResult result = adminController.MediaRequests(fakeAdmin, 1) as ViewResult;
            Assert.IsNotNull(result);

            PaginatedList<InsertRequest> actual = result.ViewData.Model as PaginatedList<InsertRequest>;
            Assert.IsNotNull(actual);

            Assert.AreEqual(pageSize, actual.Count);
            Assert.AreEqual(1, actual.PageIndex);
        }

        [TestMethod]
        public void MediaRequestsShouldReturnNotAuthorizedView()
        {
            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(false);
            mocks.ReplayAll();

            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            ViewResult result = adminController.MediaRequests(fakeAdmin, 1) as ViewResult;
           
            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateBook();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            Expect.Call(delegate { insertRequestService.Delete(insertRequest); });
            Expect.Call(delegate { insertRequestService.Save(); });
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(false);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(null);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnNotFoundView_NoBookFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = null;

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Isbn"] = "1234567890";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateBook();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateBook();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["Isbn"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Isbn"].Errors.Count);
        }

        [TestMethod]
        public void ConfirmRequestBookShouldReturnExpectedViewData_ValidationErrors()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateBook();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestBook(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }

        [TestMethod]
        public void ConfirmRequestMovieShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateMovie();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            Expect.Call(delegate { insertRequestService.Delete(insertRequest); });
            Expect.Call(delegate { insertRequestService.Save(); });
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(false);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(null);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnNotFoundView_NoMovieFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = null;

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["ReleaseDate"] = "21.08.1984";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateMovie();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";
            formCollection["ReleaseDate"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ReleaseDate"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateMovie();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["ReleaseDate"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ReleaseDate"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestMovieShouldReturnExpectedViewData_ValidationErrors()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateMovie();

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";
            formCollection["ReleaseDate"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestMovie(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }

        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnRedirectToResult()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            Expect.Call(delegate { insertRequestService.Delete(insertRequest); });
            Expect.Call(delegate { insertRequestService.Save(); });
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            RedirectToRouteResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(false);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotFoundView_NoRequestFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(null);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnNotFoundView_NoTVShowFound()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = null;

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OriginalTitle";
            formCollection["Genre"] = "Action";
            formCollection["Seasons"] = "4";
            formCollection["ShowBeginning"] = "01.01.1904";
            formCollection["ShowEnding"] = "06.07.1999";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnMediaRequests_ValidationErrorsForRequiredInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "";
            formCollection["OriginalTitle"] = "";
            formCollection["Genre"] = "";
            formCollection["Seasons"] = "";
            formCollection["ShowBeginning"] = "";
            formCollection["ShowEnding"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Title"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["OriginalTitle"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Genre"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["Seasons"].Errors.Count);
            Assert.AreNotEqual(0, result.ViewData.ModelState["ShowBeginning"].Errors.Count);
        }
        
        [TestMethod]
        public void ConfirmRequestTVShowShouldReturnMediaRequests_ValidationErrorsForUnvalidInput()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "21";
            formCollection["Seasons"] = "101";
            formCollection["ShowBeginning"] = "bogus";
            formCollection["ShowEnding"] = "bogus";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

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
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            mocks.SetFakeControllerContext(adminController);
            mocks.ReplayAll();

            FormCollection formCollection = new FormCollection();
            formCollection["Title"] = "Title";
            formCollection["OriginalTitle"] = "OrignalTitle";
            formCollection["Genre"] = "";
            formCollection["Seasons"] = "";
            formCollection["ShowBeginning"] = "";
            formCollection["ShowEnding"] = "";

            adminController.ValueProvider = formCollection.ToValueProvider();

            ViewResult result = adminController.ConfirmRequestTVShow(7, formCollection, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.ViewName);

            InsertRequest actual = ((PaginatedList<InsertRequest>)result.ViewData.Model).First<InsertRequest>();
            Assert.AreEqual(insertRequest.Id, actual.Id);
            Assert.AreEqual(insertRequest.Media.Id, actual.Media.Id);
        }

        [TestMethod]
        public void DeclineRequestShouldReturnRedirectToAction()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(insertRequest);
            Expect.Call(delegate { insertRequestService.Delete(insertRequest); });
            Expect.Call(delegate { insertRequestService.Save(); });
            mocks.ReplayAll();

            RedirectToRouteResult result = adminController.DeclineRequest(7, fakeAdmin) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(mediaRequests_ViewName, result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeclineRequestShouldReturnNotAuthorizedView()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(false);
            mocks.ReplayAll();

            ViewResult result = adminController.DeclineRequest(7, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notAuthorized_ViewName, result.ViewName);
        }

        [TestMethod]
        public void DeclineRequestShouldReturnNotFoundView()
        {
            AdminController adminController = new AdminController(membershipService, insertRequestService, mediaService);

            InsertRequest insertRequest = TestUtil.generateInsertRequest();
            insertRequest.Media = TestUtil.generateTV_Show(true);

            Expect.Call(membershipService.IsAdmin(fakeAdmin.Identity.Name)).Return(true);
            Expect.Call(insertRequestService.GetById(7)).Return(null);
            mocks.ReplayAll();

            ViewResult result = adminController.DeclineRequest(7, fakeAdmin) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(notFound_ViewName, result.ViewName);
        }
    }
}
