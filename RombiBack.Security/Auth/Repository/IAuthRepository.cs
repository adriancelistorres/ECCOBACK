using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Auth.Repsitory
{
    public interface IAuthRepository
    {
        Task<UserAuth> ValidateUser(UserDTORequest request);
    }
}
