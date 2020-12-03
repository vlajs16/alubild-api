using Domain;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ITypologyModelLogic
    {
        Task<PagedList<TypologyModel>> Get(TypologyModelParams modelParams);
        Task<TypologyModel> GetById(long id, long typologyId);
    }
}
