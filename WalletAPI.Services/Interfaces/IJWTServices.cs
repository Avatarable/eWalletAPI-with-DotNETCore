using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface IJWTServices
    {
        string GenerateToken(User user, List<string> userRoles, IList<Claim> claims);
    }
}
