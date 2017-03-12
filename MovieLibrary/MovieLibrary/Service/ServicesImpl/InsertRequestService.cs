using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;

namespace MovieLibrary.Service.ServicesImpl
{
    public class InsertRequestService : IInsertRequestService
    {
        private static readonly InsertRequestService instance = new InsertRequestService();

        private InsertRequestService()
        {
        }

        public static InsertRequestService Instance
        {
            get
            {
                return instance;
            }
        }

        public InsertRequest GetById(int id)
        {
            return InsertRequestDaoEF.Instance.GetById(id); 
        }

        public IQueryable<InsertRequest> GetAll()
        {
            return InsertRequestDaoEF.Instance.GetAll();
        }

        public void Add(InsertRequest insertRequest)
        {
            InsertRequestDaoEF.Instance.Add(insertRequest);
        }

        public void Delete(InsertRequest insertRequest)
        {
            InsertRequestDaoEF.Instance.Delete(insertRequest);
        }

        public void Save()
        {
            InsertRequestDaoEF.Instance.Save();
        }

        public IQueryable<InsertRequest> GetAllOrderedByRequestDate()
        {
            return InsertRequestDaoEF.Instance.GetAllOrderedByRequestDate();
        }
    }
}