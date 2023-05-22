using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.TeamHandler.Command
{
    public class CreateTeam : IRequest<SuccessDto>
    {
        public string name { get; set; } = default!;
        public List<int> employees { get; set; }
    }

    public class CreateTeamHandler : IRequestHandler<CreateTeam, SuccessDto>
    {
        private readonly ApplicationDbContext _context;
        public CreateTeamHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessDto> Handle(CreateTeam request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Name = request.name
            };
            
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
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDto
            {
                data =  team
            };
        }
    }
}
