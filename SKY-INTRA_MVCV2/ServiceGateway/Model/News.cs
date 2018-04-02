using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
    }
}
