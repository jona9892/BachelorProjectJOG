using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTOModel.DomainModel
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
