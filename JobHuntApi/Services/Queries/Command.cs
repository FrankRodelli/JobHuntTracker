using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobHuntApi.Services.Queries
{
    public interface ICommand
    {
        string GetUsers { get; }
        string GetUserById { get; }
        string AddUser { get; }
        string UpdateUser { get; }
        string RemoveUser { get; }
    }
    public class Command : ICommand
    {
        public string GetUsers => "Select * From [dbo].[AspNetUsers]";
        public string GetUserById => "Select * From [dbo].[AspNetUsers] Where Id= @Id";
        public string AddUser => "Insert Into  [dbo].[AspNetUsers] (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, " +
            "PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName) " +
            "Values (@Id, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, " +
            "@PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName)";
        public string UpdateUser => "Update [dbo].[AspNetUsers] set Email = @Email, PhoneNumber = @PhoneNumber, LockoutEnabled = @LockoutEnabled Where Id =@Id";
        public string RemoveUser => "Delete From [dbo].[AspNetUsers] Where Id= @Id";
    }
}
