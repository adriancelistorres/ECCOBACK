using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Repository.ROM.SEGURIDAD.MGM_Accesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.SEGURIDAD.MGM_Accesos
{
    public class AccesosServices : IAccesosServices
    {

        private readonly IAccesosRepository _accesosRepository;

        private readonly IMapper _mapper;

        public AccesosServices(IAccesosRepository accesosRepository, IMapper mapper)
        {
            _accesosRepository = accesosRepository;
            _mapper = mapper;
        }
        public async Task<List<Accesos>> GetAccesos()
        {
           var respuesta=await _accesosRepository.GetAccesos();
            return respuesta;
        }

        public async Task<Respuesta> PostAccesos(AccesosRequest accs)
        {
            var respuesta = await _accesosRepository.PostAccesos(accs);
            return respuesta;
        }
        public async Task<Respuesta> DeleteAccesos(AccesosRequest accs)
        {
            var respuesta = await _accesosRepository.DeleteAccesos(accs);
            return respuesta;
        }


        public async Task<Accesos> GetSegUsuario(string usuario)
        {
            var respuesta = await _accesosRepository.GetSegUsuario(usuario);
            return respuesta;
        }
        public async Task<List<Perfiles>> GetPerfiles()
        {
            var respuesta = await _accesosRepository.GetPerfiles();
            return respuesta;
        }
    }
}
