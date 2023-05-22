using Courseproject.API.ResponseDtos;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.TeamHandler.Command;

public class DeleteTeam : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class DeleteteamHandler : IRequestHandler<DeleteTeam, SuccessDto>
{
    private ApplicationDbContext _context;
    public DeleteteamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(DeleteTeam request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams.Include(t => t.Employees).Where(w => w.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (team == null)
        {
            return new SuccessDto
            {
                status = false,
                message = $" Team Of Id {request.Id} Not Found"
            };
        }

         _context.Teams.Remove(team);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new SuccessDto();
        
    }
}
