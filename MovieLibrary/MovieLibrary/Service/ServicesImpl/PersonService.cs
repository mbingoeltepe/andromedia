using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Daos.EntityFramework;

namespace MovieLibrary.Service.ServicesImpl
{
    public class PersonService:IPersonService
    {
        private static readonly PersonService instance = new PersonService();

        private PersonService()
        {
        }

        public static PersonService Instance
        {
            get 
            {
                return instance;
            }
        }

        public IQueryable<Author> GetAuthor(string name)
        {
            return AuthorDaoEF.Instance.GetAuthor(name);
        }


        public IQueryable<Actor> GetActor(string name)
        {
            return ActorDaoEF.Instance.GetActor(name);
        }


        #region IPersonService Members

        public void AddActor(Actor actor)
        {
            ActorDaoEF.Instance.Add(actor);
        }

        public void AddAuthor(Author person)
        {
            AuthorDaoEF.Instance.Add(person);
        }

        public void DeleteActor(Actor person)
        {
            ActorDaoEF.Instance.Delete(person);
        }

        public void DeleteAuthor(Author author)
        {
            AuthorDaoEF.Instance.Delete(author);
        }

        public void SaveAuthor()
        {
            AuthorDaoEF.Instance.Save();
        }

        public void SaveActor()
        {
            ActorDaoEF.Instance.Save();
        }


        public IQueryable<Author> GetAllAuthors()
        {
            return AuthorDaoEF.Instance.GetAll();
        }

        public IQueryable<Actor> GetAllActors()
        {
            return ActorDaoEF.Instance.GetAll();
        }

        #endregion


        public Actor GetActorByExactName(string firstname, string lastname)
        {
            return ActorDaoEF.Instance.GetActorByFullName(firstname, lastname);
        }

        #region IPersonService Members


        public Author GetAuthorByExactName(string firstname, string lastname)
        {
            return AuthorDaoEF.Instance.GetAuthorByFullName(firstname, lastname);
        }

        #endregion
    }
}