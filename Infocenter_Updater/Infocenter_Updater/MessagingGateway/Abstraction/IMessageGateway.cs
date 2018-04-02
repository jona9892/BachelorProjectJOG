using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.MessagingGateway.Abstraction
{
    public interface IMessageGateway<T>
    {
        void PublishInformation(string infoscreen);
        void PublishRSSFeed(string url);
    }
}
