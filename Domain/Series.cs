using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Series
    {
        public long Id { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public long ManufacturerId { get; set; }
        public string Name { get; set; }
    }
}
