using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Queries;

public class GetEmployeeById : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetEmployeeByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetEmployeeById request, CancellationToken cancellationToken)
    {
        var employee = await (from e in _context.Employees 
                       join a in _context.Addresses on e.Address.Id equals a.Id
                       join j in _context.Jobs on e.Job.Id equals j.Id
                       where e.Id== request.Id
                       select new Employee
                       {
                           Address = a,
                           Job = j,
                           FirstName = e.FirstName,
                           LastName = e.LastName
                       }).Include(e => e.Teams).FirstOrDefaultAsync();
        if (null == employee)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Employee Of Id {request.Id} not found"
            };
        }
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
            data = new {Employees = employee}
        };
    }
}
