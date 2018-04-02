using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModel.DomainModel
{
    public class FileImage
    {
        public FileImage()
        {
            //this.Infoscreens = new HashSet<Infoscreen>();
            this.EkstrudInfoscreens = new HashSet<Infoscreen>();
            this.TermoInfoscreens = new HashSet<Infoscreen>();
            this.InfoscreenFileImages = new HashSet<InfoscreenFileImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        //public virtual ICollection<Infoscreen> Infoscreens { get; set; }

        public virtual ICollection<Infoscreen> EkstrudInfoscreens { get; set; }
        public virtual ICollection<Infoscreen> TermoInfoscreens { get; set; }

        public virtual ICollection<InfoscreenFileImage> InfoscreenFileImages { get; set; }

    }
}
