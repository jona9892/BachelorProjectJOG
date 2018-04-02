using DTOModel;
using EasyNetQ;
using Infocenter_Updater.BLL.Abstraction;
using DTOModel.DomainModel;
using Infocenter_Updater.MessagingGateway.Abstraction;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infocenter_Updater.MessagingGateway.Implementation
{
    public class InfoscreenGateway : IMessageGateway<Infoscreen>
    {
        private readonly IInfoscreenManager infoscreenManager;


        public InfoscreenGateway(IInfoscreenManager ism)
        {
            infoscreenManager = ism;
        }        

        public void PublishInformation(string infoscreen)
        {
            List<Information> list = infoscreenManager.GetInfoscreenInformations(infoscreen);

            if (list != null)
            {
                using (var bus = RabbitHutch.CreateBus("host=localhost").Advanced)
                {
                    //Create Message
                    IMessage<List<Information>> message = new Message<List<Information>>(list);
                    message.Properties.Expiration = "600000"; // milliseconds
                    // Declare an exchange:
                    var exchange = bus.ExchangeDeclare(infoscreen + "InformationTopic", ExchangeType.Topic);

                    // Publish the message:
                    bus.Publish(exchange, infoscreen + "Information", false, message);
                }
            }
        }

        public void PublishRSSFeed(string url)
        {
            var rssfeed = infoscreenManager.GetRssFeedFromUrl(url);

            if (rssfeed != null)
            {
                using (var bus = RabbitHutch.CreateBus("host=localhost").Advanced)
                {
                    //Create Message
                    IMessage<string> message = new Message<string>(rssfeed);
                    message.Properties.Expiration = "600000"; // milliseconds
                    // Declare an exchange:
                    var exchange = bus.ExchangeDeclare("RSSFeedTopic", ExchangeType.Topic);

                    // Publish the message:
                    bus.Publish(exchange, "RSSFeed", false, message);
                }
            }
        }
    }

}
