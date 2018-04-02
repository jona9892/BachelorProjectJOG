using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.MessagingGateway.Abstraction
{
    public interface IMessagingGateway
    {
        void update(string infoscreen);
    }
}
