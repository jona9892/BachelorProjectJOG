using EasyNetQ;
using EasyNetQ.Topology;
using ServiceGateway.MessagingGateway.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.MessagingGateway.Implementation
{
    public class InfoscreenMessaging : IMessagingGateway
    {
        IBus bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false");

        public void update(string infoscreen)
        {
            using (var advancedBus = RabbitHutch.CreateBus("host=localhost").Advanced)
            {
                var message = new Message<string>(infoscreen);
                var exchange = advancedBus.ExchangeDeclare("Refresh" + infoscreen + "Exhange", ExchangeType.Direct);
                var queue = advancedBus.QueueDeclare("Refresh" + infoscreen, durable: true);
                var binding = advancedBus.Bind(exchange, queue, "Refresh" + infoscreen);
                advancedBus.Publish<string>(exchange, "Refresh" + infoscreen, false, message);
            }
        }
    }
}
