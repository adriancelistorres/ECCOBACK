﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto
{
    public class ProductoDTO
    {

        public int? intModeloEquipoID { get; set; }
        //para que sea requerido
        [Required]
        //maximo de caracteres
        [MaxLength(30)]
        public string? strModeloEquipoDesc { get; set; }
    }
}
