using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Add(T t);
        T Read(int id);
        T Update(T t);
        void Delete(T t);
    }
}
