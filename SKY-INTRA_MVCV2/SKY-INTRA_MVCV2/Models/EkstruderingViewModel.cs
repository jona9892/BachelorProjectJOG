using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class EkstruderingViewModel
    {
        public Infoscreen Infoscreen { get; set; }

        public IEnumerable<Information> Informations { get; set; }
        public IEnumerable<Information> ChosenInformations { get; set; }

        public string EkstrudFileImagePath { get; set; }        

        public IEnumerable<SmallFileImage> EkstrudFileImages { get; set; }

        public IEnumerable<FileImage> FileImages { get; set; }
    }
}