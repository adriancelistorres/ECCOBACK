using RombiBack.Entities.ROM.LOGIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN
{
    public interface ILoginServices
    {
        SEG_UsuarioBE ValidateUser(SEG_UsuarioBE usuario);

    }
}
