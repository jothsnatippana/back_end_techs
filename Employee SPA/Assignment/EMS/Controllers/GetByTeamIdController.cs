using EMS.servicelayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    [Route("employees/[action]")]
    [ApiController]
   
    public class GetByTeamIdController : ControllerBase
    {
        employee em=new employee();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<responseEmployee>>> GetByid(int id)
        {
            return Ok(await em.getbyidAsync(id));
        }
    }
}
