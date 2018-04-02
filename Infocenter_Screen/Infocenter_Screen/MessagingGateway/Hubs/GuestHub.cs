using DTOModel;
using EasyNetQ;
using EasyNetQ.Topology;
using Infocenter_Screen.Hubs.MessageTranslator;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infocenter_Screen.Hubs
{
    public class GuestHub : Hub
    {
        public MessageTranslators messagetranslator = new MessageTranslators();


        public void UpdateGuest(string infoscreen)
        {
            var bus = RabbitHutch.CreateBus("host=localhost").Advanced;

            // Declare consumer id using the clients ip-address
            string consumerId = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            // Declare an exchange:
            var exchange = bus.ExchangeDeclare("GuestTopic", ExchangeType.Topic);
            
            // Declare a queue:
            var queue = bus.QueueDeclare(infoscreen + ": " + consumerId);

            //// Bind queue to exchange:
            bus.Bind(exchange, queue, "GuestTopic");

            //Asynchronous consumer:
            bus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                List<Guest> guests = messagetranslator.TranslateGuest(body);
                Clients.All.update(guests);
            }
            ));
        }
    }
}