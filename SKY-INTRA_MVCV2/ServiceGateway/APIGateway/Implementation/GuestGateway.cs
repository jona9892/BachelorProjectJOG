using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Implementation
{
    public class GuestGateway : IGuestGateway
    {

        protected static readonly string END_POINT = "http://localhost:60000/API/Guest";
        static HttpClient client = new HttpClient();

        public HttpResponseMessage Add(Guest item)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/", item).Result;
            return response;
        }

        public HttpResponseMessage Delete(Guest item)
        {
            HttpResponseMessage response =
                client.DeleteAsync(END_POINT + "/" + item.Id).Result;
            return response;
        }

        public Guest Read(int id)
        {
            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + id).Result;
            return response.Content.ReadAsAsync<Guest>().Result;
        }

        public IEnumerable<Guest> ReadAll()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/").Result;

            return response.Content.ReadAsAsync<IEnumerable<Guest>>().Result;
        }

        public HttpResponseMessage Update(Guest item)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(END_POINT + "/" + item.Id, item).Result;
            return response;

        }

        public IEnumerable<Guest> GetTodaysGuests()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/GuestsToday").Result;

            return response.Content.ReadAsAsync<IEnumerable<Guest>>().Result;
        }
    }
}
