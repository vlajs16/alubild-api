using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DataTransferObject.OrderPhotoDto
{
    public class InsertPhotoDto
    {
        public IFormFile File { get; set; }
        public long UserId { get; set; }
        public long OrderId { get; set; }
    }
}
