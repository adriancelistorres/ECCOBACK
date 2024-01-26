using AutoMapper;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using EccoBack.Repository.ROM.ENTEL_RETAIL.Intranet_Incentivos;
using EccoBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;

namespace EccoBack.Services.ROM.ENTEL_RETAIL.Intranet_Incentivos
{
    public class IncentivosServices : IIncentivosServices
    {
        private readonly IIncentivosRepository _incentivosRepository;

        private readonly IMapper _mapper;

        public IncentivosServices(IIncentivosRepository productRepository, IMapper mapper)
        {
            _incentivosRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<IncentivoVistaDTO> GetGeneralIncentivosVistasWithDNIConfirmationFalse(string dni)
        {
            return _incentivosRepository.GetGeneralIncentivosVistasWithDNIConfirmationFalse(dni);
        }

        public IEnumerable<IncentivoVistaDTO> GetIncentivosPremios(string dni)
        {
            return _incentivosRepository.GetIncentivosPremios(dni);
        }

        public void UpdateConfirmacionEntrega(string dni, int idIncentivo)
        {
             _incentivosRepository.UpdateConfirmacionEntrega(dni,idIncentivo);

        }

        public  UserDTO ValidateUser(string dni, string password)
        {
            var incentivos =  _incentivosRepository.ValidateUser(dni, password);

            _mapper.Map<UserDTO>(incentivos);
            return _mapper.Map<UserDTO>(incentivos); ;
        }
    }
}
