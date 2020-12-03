using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICategoryLogic : IRepository<Category>
    {
        Task<ICollection<Category>> GetByTypology(long typologyId);
    }
}
