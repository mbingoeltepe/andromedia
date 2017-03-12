using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    interface IAuthorDao:IPersonDao<Author>
    {
        IQueryable<Author> GetAuthor(string name);
        Author GetAuthorByFullName(string firstname, string lastname);
    }
}
