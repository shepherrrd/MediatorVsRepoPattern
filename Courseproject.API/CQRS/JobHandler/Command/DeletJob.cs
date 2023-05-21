using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Command;

public class DeletJob : IRequest<SuccessDto>
{
    public int Id { get; set; }

}

public class DeleteJobHandler : IRequestHandler<DeletJob, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public DeleteJobHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(DeletJob request, CancellationToken cancellationToken)
    {

        var job = await _context.Jobs.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (null == job)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Address Of Id {request.Id} not found"
            };
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
        };
    }
}
