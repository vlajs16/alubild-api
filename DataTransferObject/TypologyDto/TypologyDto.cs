using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.TypologyDto
{
    public class TypologyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public bool Glass { get; set; } = false;
        public bool Guide { get; set; } = false;
        public bool Tabakera { get; set; } = false;
        public string PhotoUrl { get; set; }
    }
}
