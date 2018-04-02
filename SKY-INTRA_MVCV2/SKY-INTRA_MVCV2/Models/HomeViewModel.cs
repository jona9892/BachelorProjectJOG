using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Guest> Guests { get; set; }

        public FileImage EkstrudFileImage { get; set; }

        public FileImage TermoFileImage { get; set; }

        public IEnumerable<Information> Informations { get; set; }

        public IEnumerable<Anniversary> anniversaries { get; set; }

        public IEnumerable<FileImage> FileImages { get; set; }
    }
}