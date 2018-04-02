using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Abstraction
{
    public interface IGuestGateway : IAPIGateway<Guest>
    {
        IEnumerable<Guest> GetTodaysGuests();
    }
}
