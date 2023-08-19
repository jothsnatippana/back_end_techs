using EMS.servicelayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    [Route("teams/[action]")]
    [ApiController]
   
    public class CreateTeamController : ControllerBase
    {
        team em=new team();
        Recommendation re = new Recommendation();
        [HttpPost]
        public async Task<ActionResult<IEnumerable<responseEmployee>>> createteam(requestteam req)
        {
            return Ok(await em.createteam(req));
        }

        [HttpGet]
        public async Task<ActionResult<int>> getrecommendation()
        {
            return Ok(await re.getrecommendationsAsync());
        }


    }
}
