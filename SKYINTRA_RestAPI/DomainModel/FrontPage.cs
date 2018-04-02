using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModel
{
    public class FrontPage
    {
        public int Id { get; set; }

        public FileImage EkstruderingFileImage { get; set; }

        public FileImage TermoformFileImage { get; set; }
    }
}
