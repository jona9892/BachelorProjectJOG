using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class NewsViewModel
    {
        public News NewNews { get; set; }
        public News NewsToEdit { get; set; }
        public IEnumerable<News> News { get; set; }
    }
}