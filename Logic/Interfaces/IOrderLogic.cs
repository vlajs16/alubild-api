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
        Task<bool> Insert(Order order);
        Task<Order> GetById(long id);
        Task<PagedList<Order>> GetByUser(OrderParams orderParams);
        Task<bool> UpdateValues(Order order);
        Task<bool> UpdateScheduledDate(long orderId, DateTime date);
        Task<bool> UpdatePrice(long orderId, double price);
        Task<bool> Delete(Order order);
    }
}
