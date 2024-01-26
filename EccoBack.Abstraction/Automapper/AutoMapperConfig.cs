using AutoMapper;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace EccoBack.Abstraction.Automapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Producto, ProductoDTO>();
                cfg.CreateMap<Reports, ReportsDTO>();
                cfg.CreateMap<User, UserDTO>();
                // Agrega más configuraciones de mapeo si es necesario
            });

            return config.CreateMapper();
        }
    }
}
