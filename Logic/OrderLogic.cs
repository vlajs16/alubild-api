using DataAccessLibrary;
using Domain;
using Helpers;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class OrderLogic : IOrderLogic
    {
        private AlubildContext _context;
        public OrderLogic(AlubildContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Order order)
        {
            try
            {
                Order o = await _context.Orders.FirstOrDefaultAsync();
                if (o != null)
                    return false;
                _context.Orders.Remove(o);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>" + ex.Message);
                return false;
            }
        }

        public async Task<Order> GetById(long id)
        {

            try
            {
                Order order = await _context.Orders
                    .Include(x => x.OrderItems).ThenInclude(x => x.Category)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Typology)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Color)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Quality)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Guide)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Tabakera)
                    .Include(x => x.OrderPhotos)
                    .FirstOrDefaultAsync(x => x.Id == id);
                return order;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>" + ex.Message);
                return null;
            }
        }

        public Task<PagedList<Order>> GetByUser(OrderParams orderParams)
        {
            try
            {
                var orders = _context.Orders
                    .Include(x => x.OrderPhotos)
                    .OrderBy(x => x.DateCreated)
                    .Where(x => x.UserId == orderParams.UserId).AsQueryable();
                return PagedList<Order>.CreateAsync(orders, orderParams.PageNumber, orderParams.PageSize);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>" + ex.Message);
                return null;
            }
        }

        public async Task<bool> Insert(Order order)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == order.User.Id);
                if (user == null)
                    return false;
                order.User = user;
                foreach (var photo in order.OrderPhotos)
                {
                    photo.OrderUserId = order.UserId;
                }
                foreach (var item in order.OrderItems)
                {
                    item.OrderUserId = order.UserId;
                    item.Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == item.Category.Id);
                    item.Typology = await _context.Typologies.FirstOrDefaultAsync(x => x.Id == item.Typology.Id);
                    item.Quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Id == item.Quality.Id);
                    item.Color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == item.Color.Id);
                    if (item.Guide != null)
                        item.Guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == item.Guide.Id);
                    if (item.Tabakera != null)
                        item.Tabakera = await _context.Tabakeras.FirstOrDefaultAsync(x => x.Id == item.Tabakera.Id);
                }
                await _context.AddAsync(order);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatePrice(long orderId, double price)
        {
            try
            {
                var orderFromRepo = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (orderFromRepo == null)
                    return false;
                if (orderFromRepo.Price == price)
                    return false;
                orderFromRepo.Price = price;
                _context.Orders.Update(orderFromRepo);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateScheduledDate(long orderId, DateTime date)
        {
            try
            {
                var orderFromRepo = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (orderFromRepo == null)
                    return false;
                if (date == null) return false;
                if (orderFromRepo.SchedulingDate != null && orderFromRepo.SchedulingDate == date)
                    return false;
                orderFromRepo.SchedulingDate = date;
                _context.Orders.Update(orderFromRepo);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateValues(Order order)
        {
            try
            {
                bool modified = false;
                var orderFromRepo = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
                if (orderFromRepo == null)
                    return false;
                

                _context.Orders.Update(orderFromRepo);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }
    }
}
