using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    public interface IInsertRequestService
    {
        InsertRequest GetById(int id);
        IQueryable<InsertRequest> GetAllOrderedByRequestDate();
        IQueryable<InsertRequest> GetAll();
        void Delete(InsertRequest insertRequest);
        void Save();
    }
}
