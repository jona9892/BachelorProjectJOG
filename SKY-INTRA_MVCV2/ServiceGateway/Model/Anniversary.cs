using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class Anniversary
    {
        public DateTime date { get; set; }
        public string employee { get; set; }
        public int years { get; set; }
        public string type { get; set; }

        public string MonthName { get; set; }
    }
}
