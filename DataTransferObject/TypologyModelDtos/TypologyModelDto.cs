using DataTransferObject.TypologyDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.TypologyModelDtos
{
    public class TypologyModelDto
    {
        public long Id { get; set; }
        public long TypologyId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
