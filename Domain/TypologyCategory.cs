using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TypologyCategory
    {
        public Typology Typology { get; set; }
        public long TypologyId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
