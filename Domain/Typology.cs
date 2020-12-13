using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Typology
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Glass { get; set; } = false;
        public bool Guide { get; set; } = false;
        public bool Tabakera { get; set; } = false;
        public ICollection<TypologyModel> TypologyModels { get; set; }
    }
}
