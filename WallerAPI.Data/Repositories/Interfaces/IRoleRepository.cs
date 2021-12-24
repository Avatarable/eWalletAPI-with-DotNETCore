using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRole(string name);
        Task RemoveRole(string role);
    }
}
