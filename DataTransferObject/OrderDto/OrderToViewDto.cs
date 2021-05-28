using DataTransferObject.OrderItemsDto;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.OrderDto
{
    public class OrderToViewDto
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public string ClientsName { get; set; }
        public string ClientsSurname { get; set; }
        public string ClientsAdress { get; set; }
        public string ClientsPhoneNumber { get; set; }
        public string ClientsEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Service { get; set; } = false;
        public string Note { get; set; }
        public double? Price { get; set; }
        public DateTime? SchedulingDate { get; set; }
        public ICollection<OrderPhoto> OrderPhotos { get; set; }
        public ICollection<OrderItemsViewDto> OrderItems { get; set; }
    }
}
