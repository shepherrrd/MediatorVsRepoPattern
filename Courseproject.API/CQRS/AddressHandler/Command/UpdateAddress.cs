using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.AddressHandler.Command;

public class UpdateAddress : IRequest<SuccessDto>
{
    public int id { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string Zip { get; set; }
    public string Street { get; set; }

}

public class UpdateAddressHandler : IRequestHandler<UpdateAddress, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public UpdateAddressHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(UpdateAddress request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == request.id);

        var dto = new SuccessDto();
        if (address is null)
        {
            dto.status = false;
            dto.address = null;
            dto.message = $"Address of Id {request.id} Was Not Found ";
            return dto;
        }
        address.City = request.City;
        address.Phone = request.Phone;
        address.Street = request.Street;
        address.Zip = request.Zip;
        await _context.SaveChangesAsync();
        dto.address = address;
        return dto;
    }
}
