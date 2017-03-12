using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public class InsertRequestDaoEF : IInsertRequestDao
    {
        private static readonly InsertRequestDaoEF instance = new InsertRequestDaoEF();

        private InsertRequestDaoEF()
        {
        }

        public static InsertRequestDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        public InsertRequest GetById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                return (from insertRequest in context.InsertRequestSet
                        where insertRequest.Id == id
                        select insertRequest).Single();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public IQueryable<InsertRequest> GetAll()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return context.InsertRequestSet.AsQueryable<InsertRequest>();
        }

        public void Add(InsertRequest insertRequest)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.InsertRequestSet.AddObject(insertRequest);
        }

        public void Delete(InsertRequest insertRequest)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.InsertRequestSet.DeleteObject(insertRequest);
        }

        public void Save()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            context.SaveChanges();
        }

        public IQueryable<InsertRequest> GetAllOrderedByRequestDate()
        {
            return GetAll().OrderBy(insertRequest => insertRequest.RequestDate);
        }
    }
}