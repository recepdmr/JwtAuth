using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Services.Jwt
{
    public class JwtTokenManager : ITokenService
    {
        public async Task<string> GenerationToken(IdentityUser identityUser)
        {
            var claims = new[]
            {
                new Claim("UserName",identityUser.UserName),
                new Claim("Email",identityUser.Email)
            };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"));
            var token = new JwtSecurityToken(
                issuer: "west-world.fabrikam.com",
                audience: "heimdall.fabrikam.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
