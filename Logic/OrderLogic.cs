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
                    .Include(x => x.OrderItems).ThenInclude(x => x.TypologyModel)
                    .Include(x => x.OrderItems).ThenInclude(x => x.TypologyModel).ThenInclude(x=>x.Typology)
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

        public async Task<Order> Insert(Order order)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == order.User.Id);
                if (user == null)
                    return null;
                order.User = user;
                foreach (var photo in order.OrderPhotos)
                {
                    photo.OrderUserId = order.UserId;
                }
                foreach (var item in order.OrderItems)
                {
                    item.OrderUserId = order.UserId;
                    item.Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == item.Category.Id);
                    item.TypologyModel = await _context.TypologyModels.FirstOrDefaultAsync(x => x.Id == item.TypologyModel.Id);
                    item.Quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Id == item.Quality.Id);
                    item.Color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == item.Color.Id);
                    if(item.GlassPackage != null && item.GlassQuality != null)
                    {
                        item.GlassPackage = await _context.GlassPackages.FirstOrDefaultAsync(x => x.Id == item.GlassPackage.Id);
                        item.GlassQuality = await _context.GlassQualities.FirstOrDefaultAsync(x => x.Id == item.GlassQuality.Id);
                    }
                    if (item.Guide != null)
                        item.Guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == item.Guide.Id);
                    if (item.Series != null)
                        item.Series = await _context.Series.FirstOrDefaultAsync(x => x.Id == item.Series.Id);
                    if (item.Tabakera != null)
                        item.Tabakera = await _context.Tabakeras.FirstOrDefaultAsync(x => x.Id == item.Tabakera.Id);
                }
                await _context.AddAsync(order);
                if (await _context.SaveChangesAsync() > 0)
                    return order;
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return null;
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
                    .Include(x => x.OrderItems).ThenInclude(x => x.GlassPackage)
                    .Include(x => x.OrderItems).ThenInclude(x => x.GlassQuality)
                    .Include(x => x.OrderItems).ThenInclude(x => x.Series)
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
                    item.Quality = await _context.Qualities.FirstOrDefaultAsync(x => x.Id == item.Quality.Id);
                    item.Color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == item.Color.Id);
                    item.Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == item.Category.Id);

                    if (item.GlassPackage != null)
                        item.GlassPackage = await _context.GlassPackages.FirstOrDefaultAsync(x => x.Id == item.GlassPackage.Id);
                    if(item.GlassQuality != null)
                        item.GlassQuality = await _context.GlassQualities.FirstOrDefaultAsync(x => x.Id == item.GlassQuality.Id);
                    if(item.Series != null)
                        item.Series = await _context.Series.FirstOrDefaultAsync(x => x.Id == item.Series.Id);
                    if(item.Guide!= null)
                        item.Guide = await _context.Guides.FirstOrDefaultAsync(x => x.Id == item.Guide.Id);
                    if(item.Tabakera != null)
                        item.Tabakera = await _context.Tabakeras.FirstOrDefaultAsync(x => x.Id == item.Tabakera.Id);

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
                                itemRepo.TypologyModel = item.TypologyModel;
                                itemRepo.Width = item.Width;
                                itemRepo.Series = item.Series;
                                itemRepo.GlassQuality = item.GlassQuality;
                                itemRepo.GlassPackage = item.GlassPackage;
                                break;
                            }
                        }
                    }
                    else if (item.Delete)
                    {
                        var itemForDelete = await _context.OrderItems
                            //.Include(x => x.Color)
                            //.Include(x => x.Guide)
                            //.Include(x => x.Category)
                            //.Include(x => x.Tabakera)
                            //.Include(x => x.Quality)
                            //.Include(x => x.GlassPackage)
                            //.Include(x => x.GlassQuality)
                            //.Include(x => x.Series)
                            .FirstOrDefaultAsync(x => x.Id == item.Id);
                        orderFromRepo.OrderItems.Remove(itemForDelete);
                    }
                    else if (item.Insert)
                    {
                        item.Delete = false;
                        item.Update = false;
                        item.Insert = false;
                        orderFromRepo.OrderItems.Add(item);
                    }
                    else
                        continue;
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

        public async Task<bool> InsertPhotoUrl(OrderPhoto photo)
        {
            try
            {
                await _context.AddAsync(photo);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>> " + ex.Message);
                return false;
            }
        }
    }
}
