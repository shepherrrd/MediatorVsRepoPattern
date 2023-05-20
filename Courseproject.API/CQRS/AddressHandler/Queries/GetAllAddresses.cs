using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.AddressHandler.Queries;

public class GetAllAddresses : IRequest<AddressesDto>
{
}

public class GetAllAddressesHandler : IRequestHandler<GetAllAddresses, AddressesDto>
{
    private readonly ApplicationDbContext _context;
    public GetAllAddressesHandler(ApplicationDbContext context)
    {
        _context = context;

    }
    public async Task<AddressesDto> Handle(GetAllAddresses request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.ToListAsync();
        var dto = new AddressesDto();
        if (address is not null)
        {

            dto.status = true;
            dto.message = " Here are your Addresses ";
            dto.address = address;
            return dto;
        }

        dto.status = false;
        dto.message = " Here are your Addresses ";
        dto.address = address;
        return dto;

    }
}
