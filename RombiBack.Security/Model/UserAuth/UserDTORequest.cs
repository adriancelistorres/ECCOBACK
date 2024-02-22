using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class UserDTORequest
    {
        public string codempresa { get; set; }
        public string codpais { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
