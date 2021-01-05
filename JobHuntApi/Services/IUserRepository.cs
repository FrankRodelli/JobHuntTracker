using JobHuntApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntApi.Services
{
    public interface IUserRepository
    {
        ValueTask<User> GetById(string id);
        Task AddUser(User entity);
        Task UpdateUser(User entity, string id);
        Task RemoveUser(string id);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
