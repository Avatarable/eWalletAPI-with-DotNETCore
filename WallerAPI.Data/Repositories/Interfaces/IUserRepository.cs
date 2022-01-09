using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        Task<IdentityResult> Register(User user, string password);
        Task<User> GetUserByEmail(string email);
        Task<IdentityResult> AddUserToRole(User user, string role);
        Task<IdentityResult> AddUserClaim(User user, Claim claim);
        Task<IList<Claim>> GetUserClaims(User user);
        Task<IdentityResult> RemoveUserFromRole(User user, string role);
        
    }
}
