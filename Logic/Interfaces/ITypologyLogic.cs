using Domain;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ITypologyLogic
    {
        Task<PagedList<Typology>> GetTypologies(TypologyParams typologyParams);
        Task<Typology> GetTypology(long id);
        Task<ICollection<string>> GetTypologyNames(int categoryId);
    }
}
