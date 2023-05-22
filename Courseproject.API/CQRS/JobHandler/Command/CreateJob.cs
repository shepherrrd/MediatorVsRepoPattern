using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;

namespace Courseproject.API.CQRS.JobHandler.Command;

public class CreateJob : IRequest<SuccessDto>
{
#nullable disable

    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateJobHandler : IRequestHandler<CreateJob, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public CreateJobHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(CreateJob request, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            Name = request.Name,
            Description = request.Description,
        };
        var ent = await _context.Jobs.AddAsync(job);
        await _context.SaveChangesAsync(cancellationToken);
        return new SuccessDto
        {
            data = job
        };
    }
}
