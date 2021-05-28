using Domain;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IOrderLogic
    {
        Task<Order> Insert(Order order);
        Task<Order> GetById(long id);
        Task<PagedList<Order>> GetByUser(OrderParams orderParams);
        Task<bool> UpdateValues(long orderId, Order order);
        Task<bool> UpdatePhotos(long orderId, Order order);
        Task<bool> InsertPhotoUrl(OrderPhoto photo);
        Task<bool> UpdateScheduledDate(long orderId, DateTime date);
        Task<bool> UpdatePrice(long orderId, double price);
        Task<bool> Delete(Order order);
    }
}
