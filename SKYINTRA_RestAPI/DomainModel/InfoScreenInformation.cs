using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModel
{
    public class InfoscreenInformation
    {

        //[Key]
        //public int Id { get; set; }

        [ForeignKey("Infoscreen")]
        public int InfoscreenId { get; set; }
        public virtual Infoscreen Infoscreen { get; set; }

        [ForeignKey("Information")]
        public int InformationId { get; set; }
        public virtual Information Information { get; set; }
    }
}
