using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
