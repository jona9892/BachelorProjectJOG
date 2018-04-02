using EasyNetQ;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infocenter_Screen.MessagingGateway.Hubs
{
    public class RefreshPageHub : Hub
    {
        public void RefreshPage(string infoscreen)
        {
            var advancedBus = RabbitHutch.CreateBus("host=localhost").Advanced;

            var queue = advancedBus.QueueDeclare("Refresh" + infoscreen);
            advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                if (infoscreen == "Kantine")
                {
                    Clients.All.refreshkantine();
                }
                else if (infoscreen == "Kundeservice")
                {
                    Clients.All.refreshkundeservice();
                }
                else if (infoscreen == "Ekstrudering")
                {
                    Clients.All.refreshekstrudering();
                }
                else if (infoscreen == "Termoform")
                {
                    Clients.All.refreshtermoform();
                }

            }));


        }
    }
}