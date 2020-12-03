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
    public class CategoryLogic : Repository<Category, AlubildContext>,
        ICategoryLogic
    {
        public CategoryLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<Category>> GetByTypology(long typologyId)
        {
            try
            {
                var typology = await _context.Typologies.FirstOrDefaultAsync(x => x.Id == typologyId);
                if (typology == null)
                    return null;


                var categories = await _context.Categories
                    .Where(x => x.TypologyModelCategories
                    .Any(p => p.TypologyModel.TypologyId == typologyId))
                    .ToListAsync();

                return categories;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
