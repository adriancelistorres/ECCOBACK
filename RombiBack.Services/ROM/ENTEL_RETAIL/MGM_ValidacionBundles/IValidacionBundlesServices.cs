using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public interface IValidacionBundlesServices
    {
        Task<ValidacionBundle> GetBundlesVentas([FromBody] int intIdVentasPrincipal);
    }
}
