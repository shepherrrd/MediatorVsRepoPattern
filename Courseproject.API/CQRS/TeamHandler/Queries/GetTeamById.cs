using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.TeamHandler.Queries;

public class GetTeamById : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class GetTeamByIdHandler : IRequestHandler<GetTeamById, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetTeamByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetTeamById request, CancellationToken cancellationToken)
    {
        var t = await _context.Teams.Include(t => t.Employees).Where(w => w.Id == request.Id).Select(t => new Team
        {
            Employees = t.Employees,
            Name = t.Name,
            Id = t.Id
        }).FirstOrDefaultAsync(cancellationToken);

        if (t == null)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Team Of Id {request.Id} Not Found"
            };
        }

        return new SuccessDto
        {
            status = true,
            data = t
        };
    }
}
