using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Axapta
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public ICollection<File> files { get; set; }

    }
}
