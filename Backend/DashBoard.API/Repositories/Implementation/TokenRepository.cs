using DashBoard.API.Models.Admin;
using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DashBoard.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(AccountUser user, List<string> roles)
        {
            //Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.PermissionName)));
            //claims.AddRange(roles
            //  .Select(role => role.PermissionName)
            //    .Distinct()  // Đảm bảo rằng quyền là duy nhất
            //    .Select(permission => new Claim(ClaimTypes.Role, permission)));
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            // JWT security token paramater
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var currentTimeUtc = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),

                signingCredentials: credentials);
            // return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
