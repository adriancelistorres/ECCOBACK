using AutoMapper;
using RombiBack.Entities.ROM.LOGIN.UserType;
using RombiBack.Repository.ROM.LOGIN.MGM_Country;
using RombiBack.Repository.ROM.LOGIN.MGM_UserType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.MGM_UserType
{
    public class UsertypeServices : IUsertypeServices
    {
        private  IUsertypeRepository _usertypeRepository;

        private readonly IMapper _mapper;
        public UsertypeServices(IUsertypeRepository usertypeRepository,  IMapper mapper)
        {
            _usertypeRepository = usertypeRepository;
            _mapper = mapper;

        }

        public Task<UserType> Add(UserType entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserType> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserType>> GetAll()
        {
            return await _usertypeRepository.GetAll();
        }

        public Task<bool> Remove(UserType entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserType> Update(UserType entity)
        {
            throw new NotImplementedException();
        }
    }
}
