using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Auth.Services
{
    public interface IAuthServices
    {
        Task<UserAuth> ValidateUser(UserDTORequest request);
        Task<UserDTOResponse> RombiLoginMain(UserDTORequest request);

    }
}
