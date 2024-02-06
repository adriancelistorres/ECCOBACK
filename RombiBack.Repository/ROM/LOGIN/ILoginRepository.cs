using RombiBack.Entities.ROM.LOGIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN
{
    public interface ILoginRepository
    {
        SEG_UsuarioBE ValidateUser(SEG_UsuarioBE usuario);
    }
}
