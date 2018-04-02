using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Abstraction
{
    public interface IAPIGateway<T>
    {
        HttpResponseMessage Add(T item);
        IEnumerable<T> ReadAll();
        T Read(int id);
        HttpResponseMessage Update(T item);
        HttpResponseMessage Delete(T item);
    }
}
