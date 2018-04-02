using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class InformationViewmodel
    {
        public Information NewInformation { get; set; }
        public Information InformationToEdit { get; set; }
        public IEnumerable<Information> Informations { get; set; }
    }
}