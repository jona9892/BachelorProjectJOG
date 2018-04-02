using ServiceGateway.APIGateway.Abstraction;
using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Implementation
{
    public class ImageGateway : IAPIGateway<string>
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/Image";
        static HttpClient client = new HttpClient();

        public HttpResponseMessage Add(string item)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Delete(string item)
        {
            throw new NotImplementedException();
        }

        public string Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadAll()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/").Result;


            return response.Content.ReadAsAsync<IEnumerable<string>>().Result;
        }

        public HttpResponseMessage Update(string item)
        {
            throw new NotImplementedException();
        }
    }
}
