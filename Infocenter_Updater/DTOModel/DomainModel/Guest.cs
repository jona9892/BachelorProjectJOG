using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModel.DomainModel
{
    [Serializable]
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public DateTime Dato { get; set; }
    }
}
