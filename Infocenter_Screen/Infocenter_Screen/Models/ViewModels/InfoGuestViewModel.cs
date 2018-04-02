using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.Models.ViewModels
{
    public class InfoGuestViewModel
    {
        public IEnumerable<Guest> Guests { get; set; }

        public IEnumerable<Information> Informations { get; set; }
    }
}