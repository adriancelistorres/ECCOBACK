using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.LOGIN;
using RombiBack.Services.ROM.LOGIN;
using RombiBack.Services.ROM.LOGIN.MGM_UserType;

namespace RombiBack.Controllers.ROM.LOGIN
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginServices _loginservices;
        public LoginController(ILoginServices loginservices)
        {
            _loginservices = loginservices;
        }
        [HttpPost]
        public JsonResult ValidaUsuario([FromBody] SEG_UsuarioBE BE)
        {
            SEG_UsuarioBE productoAgregado = _loginservices.ValidateUser(BE);

            return new JsonResult(productoAgregado);
        }


    }
}
