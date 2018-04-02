using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Implementation
{
    public class InformationGateway : IInformationGateway
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/Information";
        static HttpClient client = new HttpClient();

        public HttpResponseMessage Add(Information item)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/", item).Result;
            return response;
        }

        public HttpResponseMessage Delete(Information item)
        {
            HttpResponseMessage response =
                client.DeleteAsync(END_POINT + "/" + item.Id).Result;
            return response;
        }

        public IEnumerable<Information> GetLatestInformations()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/LatestInformations").Result;

            return response.Content.ReadAsAsync<IEnumerable<Information>>().Result;
        }

        public Information Read(int id)
        {
            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + id).Result;
            return response.Content.ReadAsAsync<Information>().Result;
        }

        public IEnumerable<Information> ReadAll()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/").Result;

            var formatters = new List<MediaTypeFormatter>() {
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            };
            return response.Content.ReadAsAsync<IEnumerable<Information>>().Result;
        }

        public HttpResponseMessage Update(Information item)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(END_POINT + "/" + item.Id, item).Result;
            return response;
        }
    }
}
