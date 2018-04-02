using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Models
{
    public class PDFViewModel
    {
        public FileImage FileImage { get; set; }
        public IEnumerable<FileImage> FileImages { get; set; }

        //public SmallFileImage SmallFileImage { get; set; }
        //public IEnumerable<SmallFileImage> SmallFileImages { get; set; }

        //public List<string> productions { get; set; }
    }
}