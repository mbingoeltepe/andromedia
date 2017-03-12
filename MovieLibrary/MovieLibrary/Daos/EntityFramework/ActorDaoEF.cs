using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;
using MovieLibrary.Daos.Interfaces;
using MovieLibrary.Helpers;

namespace MovieLibrary.Daos.EntityFramework
{
    public class ActorDaoEF : AbstractPersonDaoEF<Actor>, IActorDao
    {
        private static readonly ActorDaoEF instance = new ActorDaoEF();

        private ActorDaoEF()
        {
        }

        public static ActorDaoEF Instance
        {
            get
            {
                return instance;
            }
        }

        #region IActorDao Members

        public IQueryable<Actor> GetActor(string name)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                var act = from actor in context.PersonSet.OfType<Actor>()
                          where name.Contains(actor.FirstName) &&
                          name.Contains(actor.LastName)
                          select actor;
                if (act.Count() == 0)
                {
                    act = from actor in context.PersonSet.OfType<Actor>()
                          where name.Contains(actor.FirstName) ||
                          name.Contains(actor.LastName)
                          select actor;
                }

                return act;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion


        public Actor GetActorByFullName(string firstname, string lastname)
        {
            try
            {
                MediaLibContainer context = ContextHelper<MediaLibContainer>.GetCurrentContext();
                var act = from actor in context.PersonSet.OfType<Actor>()
                          where firstname.Equals(actor.FirstName) &&
                          lastname.Equals(actor.LastName)
                          select actor;
                
                return act.Single();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}