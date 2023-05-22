using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.TeamHandler.Queries;

public class GetAllTeams : IRequest<SuccessDto>
{
}

public class GetAllTeamsHandler : IRequestHandler<GetAllTeams, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetAllTeamsHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetAllTeams request, CancellationToken cancellationToken)
    {
        var t = await _context.Teams.Include(t => t.Employees).Select(t => new Team
        {
            Employees = t.Employees,
            Name= t.Name,
            Id = t.Id
        }).ToListAsync(cancellationToken);

        if(!t.Any())
        {
            return new SuccessDto
            {
                status = false,
                message = "No Teams Found"
            };
        }

        return new SuccessDto
        {
            status = true,
            data = t
        };
    }
}
