using Courseproject.API.CQRS.EmployeeHandler.Command;
using Courseproject.API.CQRS.EmployeeHandler.Queries;
using Courseproject.API.CQRS.JobHandler.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courseproject.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeMediatorController : ControllerBase
    {
        private readonly IMediator _sender;
        public EmployeeMediatorController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployee command)
        {
            var em = await _sender.Send(command);
            if (!em.status)
            {
                return BadRequest(em);
            }

            return Ok(em);
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetEmployees([FromQuery] GetAllEmployees command)
        {
            var em = await _sender.Send(command);

            if (!em.status)
            {
                return NotFound(em);
            }

            return Ok(em);
        }

        [HttpGet("Get")]

        public async Task<IActionResult> GetEmployeeById([FromQuery] GetEmployeeById command)
        {
            var em = await _sender.Send(command);
            if (!em.status)
            {
                return NotFound(em);
            }

            return Ok(em);

        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployee command)
        {
            var em = await _sender.Send(command);
            if (!em.status)
            {
                return NotFound(em);
            }

            return Ok(em);
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployee command)
        {
           var em = await _sender.Send(command);

            if (!em.status)
            {
                return NotFound(em);
            }

            return Ok(em);
        }


    }
}
