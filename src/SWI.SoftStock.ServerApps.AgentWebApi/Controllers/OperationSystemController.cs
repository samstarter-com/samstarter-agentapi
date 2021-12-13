using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/operationSystems")]
    [ApiVersion("1.0")]
    public class OperationSystemController : ControllerBase
    {
        private readonly OperationSystemService operationSystemService;

        public OperationSystemController(OperationSystemService operationSystemService)
        {
            this.operationSystemService = operationSystemService;
        }

        [Route("", Name = "setOperationSystem")]
        [HttpPut]
        public async Task<IActionResult> AddOperationSystemAsync([FromBody] OperationSystemRequest request)
        {
            var result = await operationSystemService.AddOperationSystemAsync(request);
            return Ok(result);
        }

        [Route("operationMode", Name = "setOperationMode")]
        [HttpPut]
        public async Task<IActionResult> AddOperationModeAsync([FromBody] OperationModeRequest request)
        {
            var result = await operationSystemService.AddOperationModeAsync(request);
            return Ok(result);
        }
    }
}