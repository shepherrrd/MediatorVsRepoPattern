using Courseproject.API.CQRS.Command;
using Courseproject.API.CQRS.Queries;
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
[Route("api/v1/[controller]")]
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
    public async Task<IActionResult> DeleteAddress(AddressDelete addressDelete)
    {
        await AddressService.DeleteAddressAsync(addressDelete);
        return Ok();
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var address = await AddressService.GetAddressAsync(id);
        var addressDto = new SuccessDto();

        if (address is not null)
        {
            addressDto.address = address;
            addressDto.message = "These Are Your Addresses ";
            addressDto.status = true;
            return Ok(addressDto);
        }
        addressDto.address = address;
        addressDto.message = "No Address Found";
        addressDto.status = true;
        return NotFound(addressDto);
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
