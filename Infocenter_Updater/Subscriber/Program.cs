using DTOModel;
using DTOModel.DomainModel;
using DTOModel.DomainModel.CacheModel;
using EasyNetQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            //{
            //    var myRequest = new TextMessage { Text = "Hello Server*" };
            //    var response = bus.Request<TextMessage, TextMessage>(myRequest);
            //    Console.WriteLine(response.Text);
            //}

            //using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            //{
            //    bus.Subscribe<Message<string>>("Tester", msg => Console.WriteLine(msg));

            //    Console.ReadLine();

            //}

            //using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            //{
            //   bus.Subscribe<Message<GuestDTO>>("my_id2", HandleTextMessage, x => x.WithTopic("Guest.*"));

            //    Console.WriteLine("Listening for messages. Hit <return> to quit.");

            //    Console.ReadLine();
            //}

            using (var bus = RabbitHutch.CreateBus("host=localhost").Advanced)
            {
                string consumerId = Guid.NewGuid().ToString();

                // Declare an exchange:
                var exchange = bus.ExchangeDeclare("Guest.Fanout", ExchangeType.Fanout);

                // Declare a queue (each consumer instance should hav its own queue
                // to avoid competing consumers):
                var queue = bus.QueueDeclare("Kantine: " + consumerId);

                // Bind queue to exchange (the routing key is ignored by a fanout exchange):
                bus.Bind(exchange, queue, "Guest");

                // Synchronous consumer:
                //bus.Consume<Byte[]>(queue, (message, info) =>
                //    Console.WriteLine("Consumed: " + message.Body)
                //);

                //Asynchronous consumer:
                bus.Consume(queue, (message, properties, info) => Task.Factory.StartNew(() =>
                 {
                     String jsonified = Encoding.UTF8.GetString(message);
                     Console.WriteLine("Consumed: " + jsonified);
                     }
                ));

                Console.WriteLine("Consumer is running. Type any key to exit");
                // Remember to have the statement below inside the using statement.
                // Otherwise the bus will be disposed immediately.
                Console.ReadKey();

                //var queue = advancedBus.QueueDeclare("RefreshQueue");
                //advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
                //{
                //    String jsonified = Encoding.UTF8.GetString(body);



                //    Console.Write(jsonified);
                //}));
                //Console.WriteLine("Listening for messages. Hit <return> to quit.");
                //Console.ReadLine();
            }


            //    //advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
            //    //{                   
            //    //    var message = Encoding.UTF8.GetString(body);
            //    //}));

            //    //bus.Subscribe<TextMessage>("my_id", HandleTextMessage, x => x.WithTopic("Guest"));

            //    Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    Console.ReadLine();
            //}
        }
       
    }
}
