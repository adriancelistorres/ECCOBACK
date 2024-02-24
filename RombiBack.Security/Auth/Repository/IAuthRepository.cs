using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Security.Model.UserAuth.Modules;

namespace RombiBack.Security.Auth.Repsitory
{
    public interface IAuthRepository
    {
        //Task<UserAuth> ValidateUser(UserDTORequest request);
        Task<UserDTOResponse> RombiLoginMain(UserDTORequest request);
        Task<UserDataDTOResponse> GetUserData(UserDTORequest request);
        Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request);
        Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request);
        Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request);
    }
}
