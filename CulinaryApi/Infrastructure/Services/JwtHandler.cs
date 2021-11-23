using CulinaryApi.Core.Entieties;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CulinaryApi.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_jwtSettings.JwtIssuer,
                _jwtSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
