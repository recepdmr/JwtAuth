using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtAuth.Services
{
    public interface ITokenService
    {
        Task<string> GenerationToken(IdentityUser identityUser);
    }
}
