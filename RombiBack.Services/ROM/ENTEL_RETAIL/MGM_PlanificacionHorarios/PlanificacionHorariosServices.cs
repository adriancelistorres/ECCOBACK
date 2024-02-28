using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public class PlanificacionHorariosServices : IPlanificaionHorariosServices
    {
        private readonly IPlanificacionHorariosRepository _planificacionHorariosRepository;

        private readonly IMapper _mapper;

        public PlanificacionHorariosServices(IPlanificacionHorariosRepository planificacionHorariosRepository, IMapper mapper)
        {
            _planificacionHorariosRepository = planificacionHorariosRepository;
            _mapper = mapper;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario)
        {
            var turnosuper= await _planificacionHorariosRepository.GetTurnosSupervisor(usuario);
            return turnosuper;

        }

        public async Task<string> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper)
        {
            var turnosuper = await _planificacionHorariosRepository.PostTurnosSupervisor(turnossuper);
            return turnosuper;
           
        }
    }
}
