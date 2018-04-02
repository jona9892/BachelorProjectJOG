using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class GuestViewModel
    {
        public Guest Guest { get; set; }
        public IEnumerable<Guest> Guests { get; set; }

        public List<string> countries = new List<string>();
    }

    
}