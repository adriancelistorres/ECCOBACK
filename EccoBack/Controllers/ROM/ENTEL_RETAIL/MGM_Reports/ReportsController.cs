using EccoBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccoBack.Controllers.ROM.ENTEL_RETAIL.MGM_Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsServices _reportsServices;

        public ReportsController(IReportsServices reportsServices)
        {
            _reportsServices = reportsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reporte = await _reportsServices.GetAll();
            return Ok(reporte);
        }
    }
}
