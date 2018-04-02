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
    public class FileImageGateway : IAPIGateway<FileImage>
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/FileImage";
        static HttpClient client = new HttpClient();

        public HttpResponseMessage Add(FileImage item)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/", item).Result;
            return response;
        }

        public HttpResponseMessage Delete(FileImage item)
        {
            HttpResponseMessage response =
                client.DeleteAsync(END_POINT + "/" + item.Id).Result;
            return response;
        }

        public FileImage Read(int id)
        {
            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + id).Result;
            return response.Content.ReadAsAsync<FileImage>().Result;
        }

        public IEnumerable<FileImage> ReadAll()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/").Result;

            return response.Content.ReadAsAsync<IEnumerable<FileImage>>().Result;
        }

        public HttpResponseMessage Update(FileImage item)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(END_POINT + "/" + item.Id, item).Result;
            return response;
        }
    }
}
