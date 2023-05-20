using Courseproject.Common.Model;

namespace Courseproject.API.ResponseDtos;

public class AddressesDto
{
    public bool status { get; set; } = true;
    public string message { get; set; } = default!;

    public List<Address> address { get; set; } = default!;
}
