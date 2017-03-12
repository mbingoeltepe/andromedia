using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;
using MovieLibrary.Models;

namespace MovieLibrary.Test.Daos
{
    [TestClass]
    public abstract class GenericDaoTest<T>
    {
        protected static List<T> addedObjects = new List<T>();
        protected static IGenericDao<T> dao;

        protected abstract T GenerateObject();
        protected abstract void AddObjects(int count);
        protected abstract void DeleteObjects();
        protected abstract void AddObject(T objekt);
        protected abstract void DeleteObject(T objekt);

        [TestMethod]
        public void GetAllShouldReturnFiveMedia()
        {
            AddObjects(5);
            IQueryable<T> objects = dao.GetAll();
            Assert.AreEqual(addedObjects.Count, objects.Count<T>());
        }

        [TestMethod]
        public void GetAllShouldReturnNoObject()
        {
            IQueryable<T> objects = dao.GetAll();
            Assert.AreEqual(0, objects.Count<T>());
        }

        [TestMethod]
        public abstract void GetByIdShouldReturnObject();

        [TestMethod]
        public void GetByIdShouldReturnNull()
        {
            AddObjects(5);
            Assert.IsNull(dao.GetById(-1));
        }

        [TestMethod]
        public void AddShouldPersistObject()
        {
            AddObjects(5);

            T objekt = GenerateObject();

            int count_before = dao.GetAll().Count<T>();

            AddObject(objekt);

            int count_after = dao.GetAll().Count<T>();

            Assert.AreEqual(count_before + 1, count_after);
        }

        [TestMethod]
        public void DeleteShouldDeleteObject()
        {
            AddObjects(5);

            T objekt = addedObjects.Last<T>();

            int count_before = dao.GetAll().Count<T>();

            DeleteObject(objekt);

            int count_after = dao.GetAll().Count<T>();

            Assert.AreEqual(count_before - 1, count_after);
        }

        [TestMethod]
        public abstract void UpdateShouldSaveChanges();
    }
}
