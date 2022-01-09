﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class JWTServices : IJWTServices
    {
        private readonly IConfiguration _config;

        public JWTServices(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user, List<string> userRoles, IList<Claim> claims)
        {
            // Add claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.Email, $"{user.Email}"));

            // Add Roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // set secret key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:SecretKey").Value));

            // define security token descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            // create token
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
