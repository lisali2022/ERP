using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OneAuth.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JWTController : ControllerBase
{
    
    [Authorize]
    [HttpGet]
    public IActionResult GetJwt()
    {
        return Ok($"Name from jwt:{User.Identity?.Name}");
    }
}
