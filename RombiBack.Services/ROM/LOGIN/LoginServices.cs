using AutoMapper;
using RombiBack.Entities.ROM.LOGIN;
using RombiBack.Repository.ROM.LOGIN;
using RombiBack.Repository.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _loginRepository;

        private IMapper _mapper;

        public LoginServices(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
        }
        public SEG_UsuarioBE ValidateUser(SEG_UsuarioBE usuario)
        {
            return _loginRepository.ValidateUser(usuario);
        }
    }
}
