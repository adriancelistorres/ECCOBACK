﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth.Modules
{
    public class ItemModuloDTOResponse
    {
        public int? iditemmodulo { get; set; }
        public string? nombreitemmodulo { get; set; }
        public string? iconoitemmodulo { get; set; }
        public string? rutaitemmodulo { get; set; }
        public int? nivelitemmodulo { get; set; }
        public int? ordenitemmodulo { get; set; }
        public int? estadoitemmodulo { get; set; }
    }
}
