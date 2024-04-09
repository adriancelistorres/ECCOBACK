using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos
{
    public class Accesos
    {
        public int? idacceso { get; set; }
        public int? idperfiles { get; set; }
        public string? usuario { get; set; }

        public string? dni { get; set; }
        public string? perfil { get; set; }
        public string ?nombrecompleto { get; set; }
        //public int? estado { get; set; }
        //public string? usuario_creacion { get; set; }
        //public string? usuario_modificacion { get; set; }
        //public DateTime? fecha_creacion { get; set; }
        //public DateTime? fecha_modificacion { get; set; }
    }
}
