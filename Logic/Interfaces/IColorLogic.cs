using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IColorLogic : IRepository<Color>
    {
        Task<ICollection<Color>> GetByCategory(int id);
    }
}
