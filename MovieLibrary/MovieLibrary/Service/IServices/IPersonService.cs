using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Service.IServices
{
    interface IPersonService
    {
        void AddActor(Actor actor);
        void AddAuthor(Author person);
        void DeleteActor(Actor person);
        void DeleteAuthor(Author author);
        void SaveAuthor();
        void SaveActor();
        IQueryable<Author> GetAuthor(string name);
        IQueryable<Actor> GetActor(string name);
        Actor GetActorByExactName(string firstname, string lastname);
        Author GetAuthorByExactName(string firstname, string lastname);
        IQueryable<Author> GetAllAuthors();
        IQueryable<Actor> GetAllActors();
    }
}
