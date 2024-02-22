using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth.Modules
{
    public class SubModuloDTOResponse
    {
        public int? idsubmodulo { get; set; }
        public string? nombresubmodulo { get; set; }
        public string? iconosubmodulo { get; set; }
        public string? rutasubmodulo { get; set; }
        public int? nivelsubmodulo { get; set; }
        public int? ordensubmodulo { get; set; }
        public int? estadosubmodulo { get; set; }
        public List<ItemModuloDTOResponse>? items { get; set; }
    }
}
