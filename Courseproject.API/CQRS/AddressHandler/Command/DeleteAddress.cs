using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.AddressHandler.Command;

public class DeleteAddress : IRequest<SuccessDto>
{
    public int Id { get; set; }

}

public class DeleteAddressHandler : IRequestHandler<DeleteAddress, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public DeleteAddressHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(DeleteAddress request, CancellationToken cancellationToken)
    {

        var address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (null == address)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Address Of Id {request.Id} not found"
            };
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
        };
    }
}
