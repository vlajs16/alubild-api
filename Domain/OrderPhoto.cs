using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderPhoto
    {
        public long Id { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        public long OrderUserId { get; set; }
        public string Url { get; set; }
        public bool Important { get; set; } = false;

    }
}
