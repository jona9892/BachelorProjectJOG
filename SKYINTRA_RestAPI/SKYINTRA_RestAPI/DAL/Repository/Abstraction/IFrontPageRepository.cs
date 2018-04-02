using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IFrontPageRepository
    {
        FrontPage GetFirst();

        FrontPage Update(FrontPage t);
    }
}
