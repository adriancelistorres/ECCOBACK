using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class UserDTORequest
    {
        public string CodPais { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
}
