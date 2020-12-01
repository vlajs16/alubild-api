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
    public class QualityLogic : Repository<Quality, AlubildContext>, 
        IQualityLogic
    {
        public QualityLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<Quality>> GetByCategory(int id)
        {
            try
            {
                return await _context.Qualities
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
