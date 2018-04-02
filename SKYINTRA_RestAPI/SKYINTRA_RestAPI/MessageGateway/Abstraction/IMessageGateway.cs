using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.MessageGateway.Abstraction
{
    public interface IMessageGateway
    {
        void PublishGuest();
    }
}
