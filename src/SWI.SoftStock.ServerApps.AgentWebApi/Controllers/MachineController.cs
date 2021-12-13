using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices;
using System;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/machines")]
    [ApiVersion("1.0")]
    public class MachineController : ControllerBase
    {
        private readonly MachineService machineService;

        public MachineController(MachineService machineService)
        {
            this.machineService = machineService;
        }

        [Route("", Name = "setMachine")]
        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody] MachineRequest request)
        {
            var result = await machineService.AddAsync(request);
            return Ok(result);
        }

        [Route("{machineId}", Name = "getMachine")]
        [HttpGet]
        public async Task<IActionResult> GetDataAsync(Guid machineId)
        {
            var result = await machineService.GetDataAsync(machineId);
            return Ok(result);
        }

        [Route("activity/{machineId}", Name = "setMachineActivity")]
        [HttpPost]
        public async Task<IActionResult> SetActivityAsync(Guid machineId)
        {
            var result = await machineService.SetActivityAsync(machineId);
            return Ok(result);
        }
    }
}