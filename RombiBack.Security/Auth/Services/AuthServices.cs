using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RombiBack.Security.Auth.Repsitory;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Auth.Services
{
    public class AuthServices:IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IDistributedCache _cache;
        //private readonly ILogger<CompanyServices> _logger;

        private IMapper _mapper;

        public AuthServices(/*ILogger<CompanyServices> logger,*/ IDistributedCache cache, IAuthRepository authRepository, IMapper mapper)
        {
            //_logger = logger;

            _cache = cache;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserAuth> ValidateUser(UserDTORequest request)
        {
            var validateUser= await _authRepository.ValidateUser(request);
            return validateUser;
        }
    }
}
