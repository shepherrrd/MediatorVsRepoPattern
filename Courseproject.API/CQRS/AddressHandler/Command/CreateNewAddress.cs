using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;

namespace Courseproject.API.CQRS.AddressHandler.Command;

public class CreateNewAddress : IRequest<SuccessDto>
{
#nullable disable
    public string City { get; set; }
    public string Zip { get; set; }
    public string Street { get; set; }
    public string Phone { get; set; }
}

public class CreateNewAddressHandler : IRequestHandler<CreateNewAddress, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public CreateNewAddressHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(CreateNewAddress request, CancellationToken cancellationToken)
    {
        var address = new Address
        {
            City = request.City,
            Street = request.Street,
            Zip = request.Zip,
            Phone = request.Phone,

        };
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync(cancellationToken);
        var dto = new SuccessDto();
        dto.data = address;

        return dto;

    }
}
