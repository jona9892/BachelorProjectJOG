using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.Models
{
    public class Anniversary
    {
        public DateTime date { get; set; }
        public string employee { get; set; }
        public int years { get; set; }
        public string type { get; set; }
    }
}
