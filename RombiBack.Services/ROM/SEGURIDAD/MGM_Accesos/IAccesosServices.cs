﻿using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.SEGURIDAD.MGM_Accesos
{
    public interface IAccesosServices
    {
        Task<List<Accesos>> GetAccesos();

    }
}