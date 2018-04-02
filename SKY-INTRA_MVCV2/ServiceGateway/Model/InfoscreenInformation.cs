using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class InfoscreenInformation
    {
        public int InfoscreenId { get; set; }
        public Infoscreen Infoscreen { get; set; }
        public int InformationId { get; set; }
        public Information Information { get; set; }
    }
}
