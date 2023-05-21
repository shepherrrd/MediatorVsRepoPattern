using Courseproject.API.CQRS.AddressHandler.Command;
using Courseproject.API.CQRS.AddressHandler.Queries;
using Courseproject.API.CQRS.JobHandler.Command;
using Courseproject.API.CQRS.JobHandler.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courseproject.API.Controllers
{
    [Route("api/v1/Mediator/[controller]")]
    [ApiController]
    public class JobMediatorController : ControllerBase
    {
        private IMediator _sender;
        public JobMediatorController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create([FromBody] CreateJob command)
        {
            var job = await _sender.Send(command);

            return Ok(job);
        }
        [HttpPost("GetAll")]

        public async Task<IActionResult> GetAll(GetAllJobs command)
        {
            var jobs = await _sender.Send(command);

            if (!jobs.status)
            {
                return NotFound(jobs);
            }

            return Ok(jobs);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateJob([FromRoute] int id, [FromBody] UpdateJob address)
        {
            address.id = id;
            var ent = await _sender.Send(address);
            if (!ent.status)
            {
                return BadRequest(ent);
            }
            return Ok(ent);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAddress([FromBody] DeletJob command)
        {
            var ad = await _sender.Send(command);
            if (!ad.status)
            {
                return NotFound(ad);
            }
            return Ok(ad);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAddress([FromQuery] GetJobById command)
        {
            var address = await _sender.Send(command);
            if (!address.status)
            {
                return NotFound(address);
            }

            return Ok(address);
        }

       
    }
}
