using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.Models.ViewModels
{
    public class KundeserviceViewModel
    {
        public IEnumerable<String> Images { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public string RSSFeed { get; set; }
    }
}