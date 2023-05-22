using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Queries;

public class DeleteEmployee : IRequest<SuccessDto>
{
    public int Id { get; set; }
}

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public DeleteEmployeeHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(DeleteEmployee request, CancellationToken cancellationToken)
    {
        var employee = await (from e in _context.Employees
                              join a in _context.Addresses on e.Address.Id equals a.Id
                              join j in _context.Jobs on e.Job.Id equals j.Id
                              where e.Id == request.Id
                              select new Employee
                              {
                                  Address = a,
                                  Job = j,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName
                              }).FirstOrDefaultAsync();
        if (null == employee)
        {
            return new SuccessDto
            {
                status = false,
                message = $"Employee Of Id {request.Id} not found"
            };
        }
         _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
            
        };
    }
}
