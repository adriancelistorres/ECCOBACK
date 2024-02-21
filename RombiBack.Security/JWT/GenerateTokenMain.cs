using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RombiBack.Security.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.JWT
{
    public class GenerateTokenMain
    {
        private readonly IConfiguration _configuration;

        public GenerateTokenMain(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string dni)
        {
            var jwt = _configuration.GetSection("JWT").Get<JwtModel>();
            var claims = new[]
            {
                new Claim("dni", dni)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
