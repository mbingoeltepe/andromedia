using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IPersonDao<T> : IGenericDao<T> where T : Person
    {
    }
}