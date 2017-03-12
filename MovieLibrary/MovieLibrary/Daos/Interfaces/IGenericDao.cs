using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IGenericDao<T>
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T item);
        void Delete(T item);
        void Save();
    }
}