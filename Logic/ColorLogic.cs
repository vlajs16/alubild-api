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
    public class ColorLogic : Repository<Color, AlubildContext>,
        IColorLogic
    {

        public ColorLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<Color>> GetByCategory(int id)
        {
            try
            {
                return await _context.Colors
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
