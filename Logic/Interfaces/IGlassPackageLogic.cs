using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IGlassPackageLogic : IRepository<GlassPackage>
    {
        Task<ICollection<GlassPackage>> GetByTypology(long id);
    }
}
