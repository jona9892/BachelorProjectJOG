using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class User
    {
        public int objectid { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string realname { get; set; }
        public string email { get; set; }
    }
}
