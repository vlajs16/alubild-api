using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.TypologyDtos
{
    public class TypologyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Glass { get; set; }
        public bool Guide { get; set; }
        public bool Tabakera { get; set; }
    }
}
