using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOModel
{
    public class Infoscreen
    {
        public Infoscreen()
        {
            this.InfoscreenInformations = new HashSet<InfoscreenInformation>();
            this.InfoscreenFileImages = new HashSet<InfoscreenFileImage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<String> Images { get; set; }
        [NotMapped]
        public List<String> Videos { get; set; }

        [ForeignKey("EkstruderingFileImage")]
        public int? EkstruderingFileImageId { get; set; }
        public FileImage EkstruderingFileImage { get; set; }

        [ForeignKey("TermoformFileImage")]
        public int? TermoformFileImageId { get; set; }
        public FileImage TermoformFileImage { get; set; }

        //[ForeignKey("FileImage")]
        //public int? FileImageId { get; set; }
        //public FileImage FileImage { get; set; }

        public virtual ICollection<InfoscreenInformation> InfoscreenInformations { get; set; }

        public virtual ICollection<InfoscreenFileImage> InfoscreenFileImages { get; set; }


        public string RSSFeed { get; set; }
    }
}
