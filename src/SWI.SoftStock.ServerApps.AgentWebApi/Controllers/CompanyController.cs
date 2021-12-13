using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.ServerApps.AgentServices;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/companies")]
    [ApiVersion("1.0")]
    public class CompanyController : ControllerBase
    {
        private readonly CheckCompanyService checkCompanyService;

        public CompanyController(CheckCompanyService checkCompanyService)
        {
            this.checkCompanyService = checkCompanyService;
        }

        [Route("check/{uniqueCompanyId}", Name="checkCompany")]
        [HttpGet]
        public async Task<IActionResult> CheckAsync(string uniqueCompanyId)
        {
            var result = await checkCompanyService.CheckAsync(uniqueCompanyId);
            return Ok(result);
        }
    }
}
