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

        public async Task<bool> UpdateValues(long orderId, Order order)
        {
            try
            {
                bool modified = false;
                bool notification = false;
                var orderFromRepo = await _context.Orders
                    .Include(x => x.OrderItems).ThenInclude(x => x.Color)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Guide)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Category)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Tabakera)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Quality)
                    .FirstOrDefaultAsync(x => x.Id == orderId);
                if (orderFromRepo == null)
                    return false;
                
                if(orderFromRepo.ClientsAdress != order.ClientsAdress || orderFromRepo.ClientsEmail != order.ClientsEmail 
                    || orderFromRepo.ClientsName != order.ClientsName || orderFromRepo.ClientsPhoneNumber != order.ClientsPhoneNumber
                    || orderFromRepo.ClientsSurname != order.ClientsSurname)
                {
                    orderFromRepo.ClientsAdress = order.ClientsAdress;
                    orderFromRepo.ClientsEmail = order.ClientsEmail;
                    orderFromRepo.ClientsName = order.ClientsName;
                    orderFromRepo.ClientsPhoneNumber = order.ClientsPhoneNumber;
                    orderFromRepo.ClientsSurname = order.ClientsSurname;
                    modified = true;
                }

                foreach (var item in order.OrderItems)
                {
                    if (item.Update)
                    {
                        foreach (var itemRepo in orderFromRepo.OrderItems)
                        {
                            if (item.Id == itemRepo.Id)
                            {
                                itemRepo.Category = item.Category;
                                itemRepo.Color = item.Color;
                                itemRepo.ColorString = item.ColorString;
                                itemRepo.Guide = item.Guide;
                                itemRepo.Height = item.Height;
                                itemRepo.Note = item.Note;
                                itemRepo.Quality = item.Quality;
                                itemRepo.Quantity = item.Quantity;
                                itemRepo.Side = item.Side;
                                itemRepo.Tabakera = item.Tabakera;
                                itemRepo.Typology = item.Typology;
                                itemRepo.Width = item.Width;
                                break;
                            }
                        }
                    }
                    else if (item.Delete)
                        orderFromRepo.OrderItems.Remove(item);
                    else if (item.Insert)
                        orderFromRepo.OrderItems.Add(item);
                    else
                        continue;
                    item.Delete = false;
                    item.Update = false;
                    item.Insert = false;
                    notification = true;
                    modified = true;
                }
                if (notification)
                    Debug.WriteLine("Salje se obavestenje o izmeni naloga.");
                if (modified == false)
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

        public Task<bool> UpdatePhotos(long orderId, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
