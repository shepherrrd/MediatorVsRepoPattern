using Courseproject.Common.Dtos;
using Courseproject.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Courseproject.API.Controllers;


[Route("api/v1/Repo/Address")]
public class AddressRepoController : ControllerBase
{
    private IAddressService AddressService { get; }

    public AddressRepoController(IAddressService addressService)
    {
        AddressService = addressService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
    {
        var id = await AddressService.CreateAddressAsync(addressCreate);
        return Ok(id);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAddress(AddressUpdate addressUpdate)
    {
        await AddressService.UpdateAddressAsync(addressUpdate);
        return Ok();
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
        return Ok(address);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAddresses()
    {
        var addresses = await AddressService.GetAddressesAsync();
        return Ok(addresses);
    }
}
