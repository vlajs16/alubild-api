using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IManufacturerLogic : IRepository<Manufacturer>
    {
        Task<ICollection<Manufacturer>> GetByCategory(int id);
    }
}
