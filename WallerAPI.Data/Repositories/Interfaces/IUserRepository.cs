using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        Task Register(User user, string password, string role);
        Task<User> GetUserByEmail(string email);
        void RemoveUserFromRole(User user, string role);
    }
}
