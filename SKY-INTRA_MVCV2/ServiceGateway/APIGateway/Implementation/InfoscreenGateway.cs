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
    public class InfoscreenGateway : IInfoscreenGateway
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/Infoscreen";
        static HttpClient client = new HttpClient();

        public void DeleteFileImageRelation(InfoscreenFileImage infoscreenFileImage)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/PDFDeleteRelation/", infoscreenFileImage).Result;
        }

        public void DeleteInformationRelation(InfoscreenInformation infoscreeninformation)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/InformationDeleteRelation/", infoscreeninformation).Result;
        }

        public void InsertFileImageRelation(InfoscreenFileImage infoscreenFileImage)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/PDFRelation/", infoscreenFileImage).Result;
        }

        public void InsertInformationRelation(InfoscreenInformation infoscreeninformation)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(END_POINT + "/InformationRelation/", infoscreeninformation).Result;
        }


        public Infoscreen Read(int id)
        {
            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + id).Result;
            return response.Content.ReadAsAsync<Infoscreen>().Result;
        }

        public Infoscreen Read(string name)
        {
            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + name).Result;
            return response.Content.ReadAsAsync<Infoscreen>().Result;
        }

        public IEnumerable<Infoscreen> ReadAll()
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Update(Infoscreen item)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(END_POINT + "/" + item.Id, item).Result;
            return response;
        }
    }
}
