using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.APIGateway.Implementation
{
    public class FrontPageGateway
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/FrontPage";
        static HttpClient client = new HttpClient();

        public FrontPage GetFrontpage()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/Frontpage").Result;

            return response.Content.ReadAsAsync<FrontPage>().Result;
        }

        public HttpResponseMessage Update(FrontPage item)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(END_POINT + "/" + 1, item).Result;
            return response;
        }
    }
}
