using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.AddressHandler.Queries;

public class GetAddressById : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class GetAddressByIdHandler : IRequestHandler<GetAddressById, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetAddressByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetAddressById request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == request.Id);
        if(null == address)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Address Of Id {request.Id} not found"
            };
        }
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
            data = address
        };
    }
}
