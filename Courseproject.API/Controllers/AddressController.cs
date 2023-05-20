using Courseproject.API.CQRS.AddressHandler.Command;
using Courseproject.API.CQRS.AddressHandler.Queries;
using Courseproject.API.ResponseDtos;
using Courseproject.Common.Dtos;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Courseproject.API.Controllers;

[ApiController]
[Route("api/v1/Mediator/[controller]")]
public class AddressController : ControllerBase
{
    private IAddressService AddressService { get; }
    private IMediator _sender { get; }
    public AddressController(IAddressService addressService, IMediator sender)
    {
        AddressService = addressService;
        _sender = sender;
    }

    [HttpPost("Create")]
     public async Task<IActionResult> CreateAddress([FromBody]UpdateAddress address)
    {
        var addres = await _sender.Send(address);
        return Ok(addres);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateAddress([FromRoute]int id,[FromBody]UpdateAddress address)
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
    public async Task<IActionResult> DeleteAddress([FromBody] DeleteAddress command)
    {
        var ad = await _sender.Send(command);
        if (!ad.status)
        {
            return NotFound(ad);
        }
        return Ok(ad);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetAddress([FromQuery]int id)
    {
        var address = await _sender.Send(new GetAddressById { Id= id });
        if (!address.status)
        {
            return NotFound(address);
        }

        return Ok(address);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAddresses()
    {

        var addresses = await  _sender.Send(new GetAllAddresses { });
        if (addresses.address == null)
        {
            return NotFound(addresses);
        }

        return Ok(addresses);
        

    }
}
