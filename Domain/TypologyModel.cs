using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TypologyModel
    {
        public long Id { get; set; }
        public Typology Typology { get; set; }
        public long TypologyId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<TypologyModelCategory> TypologyModelCategories { get; set; }
    }
}
