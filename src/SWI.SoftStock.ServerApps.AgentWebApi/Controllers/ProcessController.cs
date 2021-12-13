using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/processes")]
    [ApiVersion("1.0")]
    public class ProcessController : ControllerBase
    {
        private readonly ProcessService processService;

        public ProcessController(ProcessService processService)
        {
            this.processService = processService;
        }

        [Route("", Name = "setProcess")]
        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody]ProcessRequest request)
        {
            var result = await processService.AddAsync(request);
            return Ok(result);
        }

    }
}