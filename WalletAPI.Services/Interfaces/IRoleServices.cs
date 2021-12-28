using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WallerAPI.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IdentityResult> AddRole(string name);
        Task<IdentityResult> RemoveRole(string roleName);
    }
}
