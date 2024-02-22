﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RombiBack.Security.Auth.Services;
using RombiBack.Security.Helpers;
using RombiBack.Security.JWT;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.LOGIN.Company;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RombiBack.Controllers.AuthLogin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthLoginController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public IConfiguration _configuration;
        private readonly IGenerateToken _generateToken;
        public AuthLoginController(IConfiguration configuracion, IAuthServices authServices, IGenerateToken generateToken)
        {
            _authServices = authServices;
            _configuration = configuracion;
            _generateToken = generateToken;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(UserDTORequest request)
        //{
        //    var login = await _authServices.ValidateUser(request);
        //    return Ok(login);
        //}

        [HttpPost("LoginMain")]
        public async Task<IActionResult> LoginMain(UserDTORequest request)
        {
            var login = await _authServices.RombiLoginMain(request);

            if (login.Resultado == "ACCESO CONCEDIDO" && login.Accede == 1)
            {

                string token = _generateToken.GenerateToken(request.codempresa,request.codpais, request.user);

                // Generar token
                return Ok(new
                {
                    login.Resultado,
                    login.Accede,
                    message = "Usuario válido",
                    token
                });
            }
            else
            {
                string token = "";

                return Ok(new
                {
                    login.Resultado,
                    login.Accede,
                    message = "NO QUIERES QUE ENTRE JOSSELIN",
                    token
                });
            }
            //return Ok(login);
        }

        
    }
}
