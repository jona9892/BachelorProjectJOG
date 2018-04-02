using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dato { get; set; }
    }
}
