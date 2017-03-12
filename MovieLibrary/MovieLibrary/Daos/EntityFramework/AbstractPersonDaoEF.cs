using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public class AbstractPersonDaoEF<T> : IPersonDao<T> where T : Person
    {
        #region IGenericDao<T> Members

        public T GetById(int id)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                return (from i in context.PersonSet.OfType<T>()
                        where i.Id == id
                        select i).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                return from i in context.PersonSet.OfType<T>()
                       select i;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(T item)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

                context.PersonSet.AddObject(item);
                context.SaveChanges();
            }
            catch (Exception)
            { 
                
            }
        }

        public void Delete(T item)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

                context.PersonSet.DeleteObject(item);
                context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Save()
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                context.SaveChanges();
            }
            catch (Exception)
            { }
        }

        #endregion
    }
}