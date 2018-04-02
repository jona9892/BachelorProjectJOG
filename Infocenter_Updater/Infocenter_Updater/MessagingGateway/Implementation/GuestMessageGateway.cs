using DTOModel.DomainModel;
using EasyNetQ;
using EasyNetQ.Topology;
using Infocenter_Updater.BLL.Abstraction;
using Infocenter_Updater.MessagingGateway.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.MessagingGateway.Implementation
{
    public class GuestMessageGateway : IGuestMessageGateway
    {

        private readonly IGuestManager guestManager;


        public GuestMessageGateway(IGuestManager gm)
        {
            guestManager = gm;
        }

        public void PublishGuest()
        {
            List<Guest> todaysguest = guestManager.GetTodaysGuest();

            if (todaysguest != null)
            {
                using (var bus = RabbitHutch.CreateBus("host=localhost").Advanced)
                {

                    //Create Message
                    IMessage<List<Guest>> message = new Message<List<Guest>>(todaysguest);
                    message.Properties.Expiration = "600000"; // milliseconds
                    // Declare an exchange:
                    var exchange = bus.ExchangeDeclare("GuestTopic", ExchangeType.Topic);

                    // Publish the message:
                    bus.Publish(exchange, "GuestTopic", false, message);

                    Console.WriteLine("Guests published: ");
                    foreach (var guest in todaysguest)
                    {
                        Console.WriteLine(guest.Name);
                    }
                }
            }
        }
    }
}
