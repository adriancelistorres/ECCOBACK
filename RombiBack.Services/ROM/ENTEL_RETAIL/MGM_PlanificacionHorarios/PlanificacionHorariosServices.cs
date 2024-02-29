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

        public async Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper)
        {
             var respuesta=await _planificacionHorariosRepository.PostTurnosSupervisor(turnossuper);
           return respuesta;
        }


        public async Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosRepository.PutTurnosSupervisor(turnossuper);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosRepository.DeleteTurnosSupervisor(turnossuper);
            return respuesta;
        }


        public async Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario)
        {
            var respuesta = await _planificacionHorariosRepository.GetSupervisorPDV(usuario);
            return respuesta;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosRepository.GetTurnosDisponiblePDV(turnodispo);
            return respuesta;
        }
        public async Task<Respuesta> PostTurnosPDV(List<TurnosPdvRequest> turnosPdvList)
        {
            var respuesta = await _planificacionHorariosRepository.PostTurnosPDV(turnosPdvList);
            return respuesta;
        }
        public async Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosRepository.GetTurnosAsignadosPDV(turnodispo);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv)
        {
            var respuesta = await _planificacionHorariosRepository.DeleteTurnosPDV(turnospdv);
            return respuesta;
        }
    }
}
