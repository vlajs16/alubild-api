using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TypologyModelCategory
    {
        public TypologyModel TypologyModel { get; set; }
        public long TypologyModelId { get; set; }
        public long TypologyModelTypologyId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
