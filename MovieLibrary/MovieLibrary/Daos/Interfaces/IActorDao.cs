using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    interface IActorDao : IPersonDao<Actor>
    {
        IQueryable<Actor> GetActor(string name);
        Actor GetActorByFullName(string firstname, string lastname);
    }
}
