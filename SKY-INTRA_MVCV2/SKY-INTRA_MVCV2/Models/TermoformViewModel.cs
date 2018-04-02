using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class TermoformViewModel
    {
        public Infoscreen Infoscreen { get; set; }

        public IEnumerable<Information> Informations { get; set; }

        public IEnumerable<Information> ChosenInformations { get; set; }        

        public string TermoFileImagePath { get; set; }

        public IEnumerable<SmallFileImage> TermoFileImages { get; set; }

        public IEnumerable<FileImage> FileImages { get; set; }
    }
}