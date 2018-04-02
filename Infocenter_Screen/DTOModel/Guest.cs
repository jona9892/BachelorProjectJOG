using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public DateTime Dato { get; set; }
    }
}
