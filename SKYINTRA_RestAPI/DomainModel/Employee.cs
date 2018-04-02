using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Employee
    {
        public int objectid { get; set; }
        public int departmentid { get; set; }
        public string name { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public DateTime employment { get; set; }
        public string contact { get; set; }
        public string sex { get; set; }
        public int userid { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string telephone4 { get; set; }
        public string jobtitle { get; set; }
    }
}
