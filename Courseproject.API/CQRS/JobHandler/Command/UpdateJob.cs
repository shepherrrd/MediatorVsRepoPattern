using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Command;

public class UpdateJob : IRequest<SuccessDto>
{
    public int id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

}

public class UpdateJobHandler : IRequestHandler<UpdateJob, SuccessDto>
{
    private readonly ApplicationDbContext _context;

    public UpdateJobHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(UpdateJob request, CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(c => c.Id == request.id);

        var dto = new SuccessDto();
        if (job is null)
        {
            dto.status = false;
            dto.data = null;
            dto.message = $"Job of Id {request.id} Was Not Found ";
            return dto;
        }
        job.Name = request.Name;
        job.Description = request.Description;
        await _context.SaveChangesAsync();
        dto.data = job;
        return dto;
    }
}
