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
    public class GlassPackageLogic : Repository<GlassPackage, AlubildContext>,
        IGlassPackageLogic
    {
        public GlassPackageLogic(AlubildContext context)
            : base(context)
        {

        }

        public async Task<ICollection<GlassPackage>> GetByTypology(long id)
        {
            try
            {
                var typology = await _context.Typologies.FirstOrDefaultAsync(x => x.Id == id);
                if (typology == null)
                    return null;

                var glassPackages = await _context.GlassPackages
                    .Where(x => x.GlassPackageTypologies
                    .Any(p => p.TypologyId == id))
                    .ToListAsync();

                return glassPackages;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
