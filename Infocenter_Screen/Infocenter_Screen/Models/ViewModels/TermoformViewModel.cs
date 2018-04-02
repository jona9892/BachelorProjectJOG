using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.Models.ViewModels
{
    public class TermoformViewModel
    {
        public string TermoformProd { get; set; }
        //public InfoGuestViewModel infoguestvm { get; set; }
        public IEnumerable<String> Images { get; set; }

        public IEnumerable<Guest> Guests { get; set; }

        public IEnumerable<Information> Informations { get; set; }

        public string RSSFeed { get; set; }
    }
}