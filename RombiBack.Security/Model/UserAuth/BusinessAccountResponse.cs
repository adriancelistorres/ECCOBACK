using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class BusinessAccountResponse
    {
        public string idpais { get; set; }
        public string idnegocio { get; set; }
        public string desc_negocio { get; set; }

        public string idcuenta { get; set; }
        public string desc_cuenta { get; set; }
    }
}
