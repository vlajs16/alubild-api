using DataAccessLibrary;
using Domain;
using Helpers;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly AlubildContext _context;

        public UserLogic(AlubildContext context)
        {
            _context = context;
        }

        public async Task<bool> DisableUser(long id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return false;

                user.Enabled = false;

                _context.Update(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return false;
            }
        }

        public async Task<User> Get(long id)
        {
            try
            {
                var userFromDb = await _context.Users
                    .Include(x => x.UserLogs.OrderByDescending(p => p.LogDateTime).Take(5))
                    .Include(x => x.Orders.OrderByDescending(p => p.DateCreated).Take(5))
                    .FirstOrDefaultAsync(x => x.Id == id);
                return userFromDb;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<PagedList<User>> Get(UserParams userParams)
        {
            try
            {
                var usersFromDb = _context.Users.AsQueryable();

                if (userParams.OnlyDisabled)
                    usersFromDb = usersFromDb.Where(x => !x.Enabled);

                if (userParams.OnlyEnabled)
                    usersFromDb = usersFromDb.Where(x => x.Enabled);

                if(userParams.OrderBy != null)
                {
                    switch (userParams.OrderBy)
                    {
                        case "registrationDate":
                            usersFromDb = usersFromDb.OrderByDescending(x => x.RegistrationDate);
                            break;
                        case "lastLogin":
                            usersFromDb = usersFromDb.OrderByDescending(x => x.LastLogin);
                            break;
                        case "numberOfOrders":
                            usersFromDb = usersFromDb.OrderByDescending(x => x.Orders.Count);
                            break;
                        default: usersFromDb = usersFromDb.OrderBy(x => x.Surname);
                            break;
                    }
                }

                var pagedList = await PagedList<User>
                    .CreateAsync(usersFromDb, userParams.PageNumber, userParams.PageSize);
                return pagedList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
