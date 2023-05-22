using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Queries;

public class GetAllJobs : IRequest<SuccessDto>
{

}

public class GetAllJobsHandler : IRequestHandler<GetAllJobs, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public GetAllJobsHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetAllJobs request, CancellationToken cancellationToken)
    {
        var jobs = await _context.Jobs.ToListAsync(cancellationToken);

        if(!jobs.Any())
        {
            return new SuccessDto
            {
               status = false,
               message= "No Jobs Found"
            };
        }
        return new SuccessDto
        {
           
            data = jobs
        };

    }
}
