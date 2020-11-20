using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DataTransferObject.OrderItemsDto;

namespace DataTransferObject.OrderDto
{
    public class OrderToUpdateDto
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
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
