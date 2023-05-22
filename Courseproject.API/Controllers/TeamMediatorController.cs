using Courseproject.API.CQRS.TeamHandler.Command;
using Courseproject.API.CQRS.TeamHandler.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courseproject.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamMediatorController : ControllerBase
    {
        private ISender _sender;
        public TeamMediatorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]

        public async Task<IActionResult> CreateTeam([FromBody] CreateTeam command)
        {
            var te = await _sender.Send(command);

            if(!te.status)
            {
                return BadRequest(te);
            }

            return Ok(te);
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllteams()
        {
            var t = await _sender.Send(new GetAllTeams { });

            if(!t.status)
            {
                return BadRequest(t);
            }
            return Ok(t);
        }
        [HttpGet("Get")]

        public async Task<IActionResult> GetTeamById([FromQuery] GetTeamById command)
        {
           
            var t = await _sender.Send(command);

            if(!t.status)
            {
                return BadRequest(t);
            }
            return Ok(t);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Updateteam([FromBody] UpdateTeam command)
        {
            var t = await _sender.Send(command);

            if (!t.status)
            {
                return BadRequest(t);
            }
            return Ok(t);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteTeam([FromBody] DeleteTeam command)
        {
            var t = await _sender.Send(command);

            if (!t.status)
            {
                return BadRequest(t);
            }
            return Ok(t);
        }
    }
}
