using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ISeriesLogic : IRepository<Series>
    {
        Task<ICollection<Series>> GetByManufacturer(long id);
    }
}
