using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Manufacturer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
