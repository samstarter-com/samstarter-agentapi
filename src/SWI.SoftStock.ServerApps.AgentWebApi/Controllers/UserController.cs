using Microsoft.AspNetCore.Mvc;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentWebApi.Controllers
{
    [ApiController]
    [Route("agent/api/users")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [Route("", Name = "setUser")]
        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody]UserRequest request)
        {
            var result = await userService.AddAsync(request);
            return Ok(result);
        }

    }
}