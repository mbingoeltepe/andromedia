using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using MovieLibrary.Daos.EntityFramework;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public class InsertRequestDaoEFTest : GenericDaoTest<InsertRequest>
    {
        protected override InsertRequest GenerateObject()
        {
            return TestUtil.generateInsertRequest();
        }

        protected override void AddObjects(int count)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            for (int i = 1; i <= count; i++)
            {
                InsertRequest insertRequest = GenerateObject();
                insertRequest.User = context.UserSet.First<User>();
                context.InsertRequestSet.AddObject(insertRequest);
                addedObjects.Add(insertRequest);
            }
            context.SaveChanges();
        }

        protected override void DeleteObjects()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            foreach (InsertRequest insertRequest in addedObjects)
            {
                context.DeleteObject(insertRequest);
            }
            context.SaveChanges();

            addedObjects.Clear();
        }

        protected override void AddObject(InsertRequest insertRequest)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            insertRequest.User = context.UserSet.First<User>();
            dao.Add(insertRequest);
            dao.Save();
            addedObjects.Add(insertRequest);
        }

        protected override void DeleteObject(InsertRequest insertRequest)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
            insertRequest.User = context.UserSet.First<User>();
            dao.Delete(insertRequest);
            dao.Save();
            addedObjects.Remove(insertRequest);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestUtil.CreateDummyHttpContext();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteObjects();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            TestUtil.ClearDatabase();
            TestUtil.CreateUser();
            dao = InsertRequestDaoEF.Instance;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TestUtil.ClearDatabase();
        }

        [TestMethod]
        public override void GetByIdShouldReturnObject()
        {
            AddObjects(5);

            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            InsertRequest insertRequest_expected = context.InsertRequestSet.First<InsertRequest>();
            InsertRequest insertRequest = dao.GetById(insertRequest_expected.Id);

            Assert.AreEqual(insertRequest.Id, insertRequest_expected.Id);
        }

        [TestMethod]
        public override void UpdateShouldSaveChanges()
        {
            AddObjects(5);

            InsertRequest expected = addedObjects.Last<InsertRequest>();

            expected.RequestDate = DateTime.Now;

            dao.Save();

            InsertRequest actual = dao.GetById(expected.Id);

            Assert.AreEqual(expected.RequestDate, actual.RequestDate);
        }

        [TestMethod]
        public void GetAllOrderedByRequestDateShouldReturnOrderedRequests()
        {
            InsertRequest insertRequest1 = GenerateObject();
            InsertRequest insertRequest2 = GenerateObject();
            InsertRequest insertRequest3 = GenerateObject();
            InsertRequest insertRequest4 = GenerateObject();
            InsertRequest insertRequest5 = GenerateObject();

            AddObject(insertRequest5);
            AddObject(insertRequest3);
            AddObject(insertRequest1);
            AddObject(insertRequest2);
            AddObject(insertRequest4);

            IQueryable<InsertRequest> insertRequests = InsertRequestDaoEF.Instance.GetAllOrderedByRequestDate();

            using (IEnumerator<InsertRequest> enumInsertRequests = insertRequests.GetEnumerator())
            {
                enumInsertRequests.MoveNext();
                Assert.AreEqual(insertRequest1.RequestDate, enumInsertRequests.Current.RequestDate);
                enumInsertRequests.MoveNext();
                Assert.AreEqual(insertRequest2.RequestDate, enumInsertRequests.Current.RequestDate);
                enumInsertRequests.MoveNext();
                Assert.AreEqual(insertRequest3.RequestDate, enumInsertRequests.Current.RequestDate);
                enumInsertRequests.MoveNext();
                Assert.AreEqual(insertRequest4.RequestDate, enumInsertRequests.Current.RequestDate);
                enumInsertRequests.MoveNext();
                Assert.AreEqual(insertRequest5.RequestDate, enumInsertRequests.Current.RequestDate);
            }
        }
    }
}
