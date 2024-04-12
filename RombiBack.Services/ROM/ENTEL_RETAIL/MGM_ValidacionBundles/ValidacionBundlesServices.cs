using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public class ValidacionBundlesServices : IValidacionBundlesServices
    {
        private readonly IValidacionBundlesRepository _validacionBundlesRepository;

        public ValidacionBundlesServices(IValidacionBundlesRepository validacionBundlesRepository)
        {
            _validacionBundlesRepository = validacionBundlesRepository;
        }
        public async Task<ValidacionBundle> GetBundlesVentas(int intIdVentasPrincipal)
        {
            var respuesta = await _validacionBundlesRepository.GetBundlesVentas(intIdVentasPrincipal);
            return respuesta;
        }
    }
}
