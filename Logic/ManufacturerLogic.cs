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
    public class ManufacturerLogic : Repository<Manufacturer, AlubildContext>, IManufacturerLogic
    {
        public ManufacturerLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<Manufacturer>> GetByCategory(int id)
        {
            try
            {
                return await _context.Manufacturers
                    .Where(x => x.Category.Id == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(">>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
