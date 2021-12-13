using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/softwares")]
    [ApiVersion("1.0")]
    public class SoftwareController : ControllerBase
    {
        private readonly SoftwareService softwareService;

        public SoftwareController(SoftwareService softwareService)
        {
            this.softwareService = softwareService;
        }

        [Route("", Name = "setSoftware")]
        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody] SoftwareRequest request)
        {
            var result = await softwareService.AddAsync(request);
            return Ok(result);
        }
    }
}