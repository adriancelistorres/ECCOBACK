using RombiBack.Security.Model.UserAuth;
using RombiBack.Security.Model.UserAuth.Modules;
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
        Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request);
        Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request);
        Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request);

    }
}
