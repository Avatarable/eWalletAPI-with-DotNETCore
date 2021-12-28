using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IdentityResult> AddRole(string name);
        Task<IdentityResult> RemoveRole(string role);
        Task<IList<string>> GetUserRoles(User user);
    }
}
