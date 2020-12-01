using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(object id);
    }
}
