using Dapper;
using JobHuntApi.Models;
using JobHuntApi.Services.Queries;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntApi.Services
{
    public class UserRepository : UserService, IUserRepository
    {
        private readonly ICommand _commandText;

        public UserRepository(IConfiguration configuration, ICommand commandText) : base(configuration)
        {
            _commandText = commandText;

        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {

            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<User>(_commandText.GetUsers);
                return query;
            });

        }

        public async ValueTask<User> GetById(string id)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryFirstOrDefaultAsync<User>(_commandText.GetUserById, new { Id = id });
                return query;
            });

        }

        public async Task AddUser(User entity)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddUser,
                    new {
                        Id = entity.Id,
                        Email = entity.Email,
                        EmailConfirmed = entity.EmailConfirmed,
                        PasswordHash = entity.PasswordHash,
                        SecurityStamp = entity.SecurityStamp,
                        PhoneNumber = entity.PhoneNumber,
                        PhoneNumberConfirmed = entity.PhoneNumberConfirmed, 
                        TwoFactorEnabled = entity.TwoFactorEnabled, 
                        LockoutEndDateUtc = entity.LockoutEndDateUtc, 
                        LockoutEnabled = entity.LockoutEnabled, 
                        AccessFailedCount = entity.AccessFailedCount, 
                        UserName = entity.UserName });
                    });

        }
        public async Task UpdateUser(User entity, string id)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.UpdateUser,
                    new { Email = entity.Email, PhoneNumber = entity.PhoneNumber, LockoutEnabled = entity.LockoutEnabled});
            });

        }

        public async Task RemoveUser(string id)
        {

            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveUser, new { Id = id });
            });

        }


    }
}
