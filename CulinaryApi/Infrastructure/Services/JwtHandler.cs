using CulinaryApi.Infrastructure.DTO;
using CulinaryApi.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
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
        public JwtDto CreateToken(int userId, string role)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimeStamp().ToString()),
            };

            var expires = now.AddMinutes(_jwtSettings.JwtExpireMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.JwtIssuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimeStamp()
            };
        }
    }
}
