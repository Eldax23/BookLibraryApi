using Demo.Models.ViewModels;
using Demo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AuthModel result = await authService.LoginAsync(model);
                if (result.IsAuthenticated)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRole(AddRoleModel model)
        {

            if (ModelState.IsValid)
            {
                string result = await authService.AddRoleAsync(model);
                if(string.IsNullOrEmpty(result))
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(ModelState);

        }
    }
}
