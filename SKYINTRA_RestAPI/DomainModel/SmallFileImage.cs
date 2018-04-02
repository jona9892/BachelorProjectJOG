using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class SmallFileImage
    {
        //public SmallFileImage()
        //{
        //    this.EkstrudInfoscreens = new HashSet<Infoscreen>();
        //    this.TermoInfoscreens = new HashSet<Infoscreen>();
        //}

        public int Id { get; set; }
        public string Title { get; set;  }
        public string Production { get; set; }
        public string Path { get; set; }

        //public virtual ICollection<Infoscreen> EkstrudInfoscreens { get; set; }
        //public virtual ICollection<Infoscreen> TermoInfoscreens { get; set; }

    }
}
