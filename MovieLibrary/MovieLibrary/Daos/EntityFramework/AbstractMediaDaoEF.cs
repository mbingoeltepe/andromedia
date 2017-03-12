using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Models;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public abstract class AbstractMediaDaoEF<T> : IMediaDao<T> where T : Media
    {
        public T GetById(int id)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            try
            {
                return (from media in context.MediaSet.OfType<T>()
                        where media.Id == id
                        select media).Single();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public IQueryable<T> GetAll()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return context.MediaSet.OfType<T>();
        }

        public IQueryable<T> GetByTitle(string titleOrOriginaltitle)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            return from media in context.MediaSet.OfType<T>()
                   where (media.Title.Contains(titleOrOriginaltitle) ||
                         media.OriginalTitle.Contains(titleOrOriginaltitle))
                         && media.Pending == false
                   select media;
        }

        public void Add(T media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();

            context.MediaSet.AddObject(media);
            context.SaveChanges();
        }

        public void Delete(T media)
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
            context.MediaSet.DeleteObject(media);
            context.SaveChanges();
        }

        public void Save()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            context.SaveChanges();
        }

        public IQueryable<T> GetLast10AddedTitles()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            return (from i in context.MediaSet.OfType<T>()
                    orderby i.AddingDate descending
                    where i.Pending == false
                    select i).Take(10);
        }

        public IQueryable<T> GetTopRatedMedia()
        {
            MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
            
            return (from i in context.MediaSet.OfType<T>()
                    orderby i.AverageRating descending
                    select i).Take(10);
        }
    }
}