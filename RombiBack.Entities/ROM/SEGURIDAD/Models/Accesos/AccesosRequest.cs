using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos
{
    public class AccesosRequest
    {
        public int? idperfil { get; set; }
        //public int? idperfiles { get; set; }
        public string? dni { get; set; }
        public string? usuariocreacion { get; set; }
        //public int? estado { get; set; }
    }
}
