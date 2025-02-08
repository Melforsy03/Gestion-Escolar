using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Application.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions)
        {
            _jwtSettings = jwtSettingsOptions.Value;
        }

        public string GenerateToken(User user, string userRole)
        {
            var key = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
           {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
              new Claim(ClaimTypes.Role, userRole),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           };

            var securityToken = new JwtSecurityToken
            (
                signingCredentials: key,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
