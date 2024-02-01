using AutoMapper;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports
{
    public class ReportsServices : IReportsServices
    {
        private readonly IReportsRepository _reportRepository;

        private readonly IMapper _mapper;

        public ReportsServices(IReportsRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;

        }
        #region SERVICES-PADRE

        public Task<ReportsDTO> Add(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReportsDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

     
        public Task<bool> Remove(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReportsDTO> Update(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        #endregion Atributos

        public async Task<List<ReportsDTO>> GetAll()
        {
            var reportes = await _reportRepository.GetAll();
            return _mapper.Map<List<ReportsDTO>>(reportes);
        }


    }
}
