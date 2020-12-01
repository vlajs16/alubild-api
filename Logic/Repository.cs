using DataAccessLibrary;
using Domain;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Repository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext
    {
        protected readonly AlubildContext _context;
        private DbSet<T> _entities;
        public Repository(AlubildContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            try
            {
                return await _entities.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        //public async Task<ICollection<object>> GetByCategory(int id)
        //{
        //    try
        //    {
        //        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        //        if (category == null)
        //            return null;


        //        if(typeof(T) == typeof(Color))
        //            return await _context.Colors.Where(x => x.Category.Id == id).Cast<object>().ToListAsync();

        //        if (typeof(T) == typeof(Quality))
        //            return await _context.Qualities.Where(x => x.Category.Id == id).Cast<object>().ToListAsync();

        //        if (typeof(T) == typeof(Manufacturer))
        //            return await _context.Manufacturers.Where(x => x.Category.Id == id).Cast<object>().ToListAsync();


        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
        //        return null;
        //    }
        //}

        public virtual async Task<T> GetById(object id)
        {
            try
            {
                return await _entities.FindAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        //public async Task<ICollection<Series>> GetByManufacturer(long id)
        //{
        //    try
        //    {
        //        var manufact = await _context.Manufacturers.FirstOrDefaultAsync(x => x.Id == id);
        //        if (manufact == null)
        //            return null;

        //        var series = await _context.Series
        //            .Where(x => x.ManufacturerId == id)
        //            .Include(x => x.Manufacturer)
        //            .ToListAsync();

        //        return series;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
        //        return null;
        //    }
        //}
    }
}
