using Courseproject.API.CQRS.IdentityHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courseproject.API.Controllers
{
    [Route("api/OAuth/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ISender _sender;
        public IdentityController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("Signup")]

        public async Task<IActionResult> Signup([FromBody]RegisterUser command)
        {
            var user = await _sender.Send(command);

            if (!user.status)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUser command)
        {
            var user = await _sender.Send(command);
            if(!user.status)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}
