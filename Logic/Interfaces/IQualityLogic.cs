using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IQualityLogic : IRepository<Quality>
    {
        Task<ICollection<Quality>> GetByCategory(int id);
    }
}
