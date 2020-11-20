using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.OrderDto
{
    public class OrderToInsertDto
    {
        public long UserId { get; set; }
        public string ClientsName { get; set; }
        public string ClientsSurname { get; set; }
        public string ClientsAdress { get; set; }
        public string ClientsPhoneNumber { get; set; }
        public string ClientsEmail { get; set; }
        public bool Service { get; set; } = false;
        public string Note { get; set; }
        public double Price { get; set; }
        public string Valute { get; set; }
        public DateTime? SchedulingDate { get; set; }
        public ICollection<OrderPhoto> OrderPhotos { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
