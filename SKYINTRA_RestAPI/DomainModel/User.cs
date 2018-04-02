using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class User
    {
        public int objectid { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string realname { get; set; }
        public string email { get; set; }

        public UserGroup group { get; set; }
    }
}
