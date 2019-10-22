using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    [Authorize]
    public class JwtAuthControllerBase : ControllerBase
    {
        
    }
}
