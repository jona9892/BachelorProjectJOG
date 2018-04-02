using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class KantineViewModel
    {
        public Infoscreen Infoscreen { get; set; }

        public IEnumerable<Information> Informations { get; set; }
        public IEnumerable<Information> ChosenInformations { get; set; }

        public FileImage EkstrudFileImagePath { get; set; }

        public FileImage TermoFileImagePath { get; set; }

        public IEnumerable<FileImage> FileImages { get; set; }
        public IEnumerable<FileImage> ChosenFileImages { get; set; }
    }
}