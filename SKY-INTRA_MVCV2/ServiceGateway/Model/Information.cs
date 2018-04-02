using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class Information
    {
        public Information()
        {
            this.InfoScreenInformations = new HashSet<InfoscreenInformation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<InfoscreenInformation> InfoScreenInformations { get; set; }
    }
}
