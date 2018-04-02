using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class FrontPage
    {
        public int Id { get; set; }

        public FileImage EkstruderingFileImage { get; set; }

        public FileImage TermoformFileImage { get; set; }
    }
}
