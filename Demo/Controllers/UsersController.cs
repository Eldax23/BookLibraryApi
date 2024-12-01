using Demo.Models.ViewModels;
using Demo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService authService;

        public UsersController(IAuthService _authService)
        {
            authService = _authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AuthModel authModel = await authService.RegisterAsync(model);
                if (authModel.IsAuthenticated)
                {
                    return Ok(authModel);
                }
                else
                {
                    return BadRequest(authModel.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
