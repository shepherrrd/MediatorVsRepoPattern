using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.AddressHandler.Queries;

public class GetAllAddresses : IRequest<SuccessDto>
{
}

public class GetAllAddressesHandler : IRequestHandler<GetAllAddresses, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetAllAddressesHandler(ApplicationDbContext context)
    {
        _context = context;

    }
    public async Task<SuccessDto> Handle(GetAllAddresses request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.ToListAsync();
        var dto = new SuccessDto();
        //var em = new HttpContextAccessor().HttpContext?.User.Identity?.Name ?? "";

        if (address is not null)
        {

            dto.status = true;
            dto.message = " Here are your Addresses ";
            dto.data = new
            {
                addresses = address,
               
            };
            return dto;
        }

        dto.status = false;
        dto.message = " Here are your Addresses ";
        dto.data = address;
        return dto;

    }
}
