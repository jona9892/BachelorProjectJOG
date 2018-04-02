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
    public class EmployeeGateway : IEmployeeGateway
    {
        protected static readonly string END_POINT = "http://localhost:60000/API/Employee";
        static HttpClient client = new HttpClient();

        public Employee GetEmployee(string name)
        {
            string employeeName = name.Split('\\')[1];

            HttpResponseMessage response =
            client.GetAsync(END_POINT + "/" + employeeName).Result;
            return response.Content.ReadAsAsync<Employee>().Result;
        }

        IEnumerable<Anniversary> IEmployeeGateway.GetAnniversaries()
        {
            HttpResponseMessage response =
                client.GetAsync(END_POINT + "/AnniversaryWeek").Result;

            return response.Content.ReadAsAsync<IEnumerable<Anniversary>>().Result;
        }
    }
}
