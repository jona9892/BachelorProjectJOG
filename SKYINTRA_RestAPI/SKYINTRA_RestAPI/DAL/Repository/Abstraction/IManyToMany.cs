using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IManyToMany<T,I>
    {
        void InsertRelationship(int tId, int iId);
        void DeleteRelationship(int FirstId, int SecondId);
    }
}
