using EasyNetQ;
using EasyNetQ.Topology;
using Infocenter_Screen.Hubs.MessageTranslator;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infocenter_Screen.MessagingGateway.Hubs
{
    public class RssfeedHub : Hub
    {
        public MessageTranslators messagetranslator = new MessageTranslators();

        public void UpdateRssfeed(string infoscreen)
        {
            var bus = RabbitHutch.CreateBus("host=localhost").Advanced;

            // Declare consumer id using the clients ip-address
            string consumerId = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            // Declare an exchange:
            var exchange = bus.ExchangeDeclare("RSSFeedTopic", ExchangeType.Topic);

            // Declare a queue:
            var queue = bus.QueueDeclare(infoscreen + ": " + consumerId);

            //// Bind queue to exchange:
            bus.Bind(exchange, queue, "RSSFeed");

            //Asynchronous consumer:
            bus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                string rssfeed = messagetranslator.TranslateString(body);
                Clients.All.update(rssfeed);
            }
            ));
        }
    }
}