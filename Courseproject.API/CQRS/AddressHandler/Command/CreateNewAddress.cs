using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;

namespace Courseproject.API.CQRS.AddressHandler.Command;

public class CreateNewAddress : IRequest<SuccessDto>
{
    public Address address { get; set; }
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
            City = request.address.City,
            Street = request.address.Street,
            Zip = request.address.Zip,
            Phone = request.address.Phone,

        };
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync(cancellationToken);
        var dto = new SuccessDto();
        dto.address = request.address;

        return dto;

    }
}
