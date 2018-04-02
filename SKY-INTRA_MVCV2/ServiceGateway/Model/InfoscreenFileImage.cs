using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class InfoscreenFileImage
    {
        [ForeignKey("Infoscreen")]
        public int InfoscreenId { get; set; }
        public virtual Infoscreen Infoscreen { get; set; }

        [ForeignKey("FileImage")]
        public int FileImageId { get; set; }
        public virtual FileImage FileImage { get; set; }
    }
}
