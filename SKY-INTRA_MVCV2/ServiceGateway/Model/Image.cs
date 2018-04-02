using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Model
{
    public class Image
    {
        public int Id { get; set; }
        [DisplayName("Upload File")]
        public string Path { get; set; }
        
    }
}
