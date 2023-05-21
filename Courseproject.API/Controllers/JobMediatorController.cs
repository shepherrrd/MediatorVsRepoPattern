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
    }
}
