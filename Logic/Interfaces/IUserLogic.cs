using Domain;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IUserLogic
    {
        Task<User> Get(long id);
        Task<PagedList<User>> Get(UserParams userParams);
        Task<bool> DisableUser(long id);
    }
}
