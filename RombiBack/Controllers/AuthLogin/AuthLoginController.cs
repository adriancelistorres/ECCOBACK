using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Security.Auth.Services;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.LOGIN.Company;

namespace RombiBack.Controllers.AuthLogin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthLoginController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthLoginController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTORequest request)
        {
            var login = await _authServices.ValidateUser(request);
            return Ok(login);
        }

        [HttpPost("LoginMain")]
        public async Task<IActionResult> LoginMain(UserDTORequest request)
        {
            var login = await _authServices.RombiLoginMain(request);
            return Ok(login);
        }
    }
}
