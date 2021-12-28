using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class RoleServices : IRoleServices
    {
        private readonly IUnitOfWork _work;
        public RoleServices(IUnitOfWork work)
        {
            _work = work;
        }
        public async Task<IdentityResult> AddRole(string name)
        {
            return await _work.Roles.AddRole(name);
        }

        public async Task<IdentityResult> RemoveRole(string roleName)
        {
            return await _work.Roles.RemoveRole(roleName);
        }
    }
}
