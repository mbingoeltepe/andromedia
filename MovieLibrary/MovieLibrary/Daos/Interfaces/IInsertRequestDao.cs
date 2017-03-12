using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieLibrary.Models;

namespace MovieLibrary.Daos.Interfaces
{
    public interface IInsertRequestDao : IGenericDao<InsertRequest>
    {
        IQueryable<InsertRequest> GetAllOrderedByRequestDate();
    }
}
