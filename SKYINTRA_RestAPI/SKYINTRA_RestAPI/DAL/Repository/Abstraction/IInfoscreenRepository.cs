using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IInfoscreenRepository
    {
        Infoscreen Read(int id);
        Infoscreen Read(string name);
        IEnumerable<Infoscreen> ReadAll();
        Infoscreen Update(Infoscreen infoscreen);
    }
}
