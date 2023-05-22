using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Queries;

public class GetJobById : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class GetJobByIdHandler : IRequestHandler<GetJobById, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetJobByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetJobById request, CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(c => c.Id == request.Id);
        if(null == job)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Job Of Id {request.Id} not found"
            };
        }
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
            data = job
        };
    }
}
