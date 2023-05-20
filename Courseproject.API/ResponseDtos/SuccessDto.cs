using Courseproject.Common.Model;

namespace Courseproject.API.ResponseDtos;

public class SuccessDto
{
    public bool status { get; set; } = true;
    public string message { get; set; } = "The Request Was Successfull";

    public Address address { get; set; } = default!;
}
