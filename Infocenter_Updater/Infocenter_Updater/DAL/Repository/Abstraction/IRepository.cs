
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.DAL.Repository.Abstraction
{
    public interface IRepository<T,K>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);

        IEnumerable<K> ReadAllCachedObjects(string infoscreen);

        void CacheObjects(List<K> t);

        void DeleteCachedObjects(List<Guid> t);
    }
}
