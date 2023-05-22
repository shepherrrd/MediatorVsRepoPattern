using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Courseproject.API.CQRS.TeamHandler.Command;

public class UpdateTeam : IRequest<SuccessDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> employees { get; set; }
}

public class UpdateTeamHandler : IRequestHandler<UpdateTeam, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public UpdateTeamHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(UpdateTeam request, CancellationToken cancellationToken)
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
        var employee = await _context.Employees.Where(e => request.employees.Contains(e.Id)).ToListAsync();
        var nonexisting = request.employees.Except(employee.Select(e => e.Id));
        if (nonexisting.Any())
        {
            return new SuccessDto
            {
                status = false,
                message = $"The Employee(s) of Id ({string.Join(", ", nonexisting)}) Do Not Exist"
            };
        }
        team.Employees = employee;
        team.Name = request.Name;
        //await _context.SaveChangesAsync();
      

            
        return new SuccessDto
        {
            status = true,
            data =  team
        };
    }
}
