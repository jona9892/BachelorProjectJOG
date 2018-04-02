using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IGuestRepository : IRepository<Guest>
    {
        IEnumerable<Guest> ReadTodaysGuests();
    }
}
