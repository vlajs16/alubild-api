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
    public class SeriesLogic : Repository<Series, AlubildContext>, 
        ISeriesLogic
    {
        public SeriesLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<Series>> GetByManufacturer(long id)
        {
            try
            {
                var manufact = await _context.Manufacturers.FirstOrDefaultAsync(x => x.Id == id);
                if (manufact == null)
                    return null;

                var series = await _context.Series
                    .Where(x => x.ManufacturerId == id)
                    .Include(x => x.Manufacturer)
                    .ToListAsync();

                return series;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }

        public override async Task<Series> GetById(object id)
        {
            try
            {
                return await _context.Series.FirstOrDefaultAsync(x => x.Id == (long)id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
