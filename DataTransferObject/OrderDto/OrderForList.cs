using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.OrderDto
{
    public class OrderForList
    {
        public long Id { get; set; }
        public string ClientsName { get; set; }
        public string ClientsSurname { get; set; }
        public string ClientsPhoneNumber { get; set; }
        public string ClientsEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? SchedulingDate { get; set; }
    }
}
