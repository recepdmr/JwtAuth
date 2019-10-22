using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Controllers
{
    [AllowAnonymous]
    public class AuthController : JwtAuthControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {

                    var token = await _tokenService.GenerationToken(user);
                    await _userManager.SetAuthenticationTokenAsync(user, "APP", "Auth", token);
                    return Ok(token);
                }
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel registerModel)
        {
            var user = await _userManager.CreateAsync(new IdentityUser { Email = registerModel.Email, UserName = registerModel.UserName }, registerModel.Password);
            if (user.Succeeded)
            {
                return Ok(user);
            }
            else return BadRequest(JsonConvert.SerializeObject(user.Errors));
        }
    }
}
