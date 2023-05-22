using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.JobHandler.Queries;

public class UpdateEmployee : IRequest<SuccessDto>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public int AddressId { get; set; }

    public int JobId { get; set; }
}

public class UpdateEmployeehandler : IRequestHandler<UpdateEmployee, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public UpdateEmployeehandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(UpdateEmployee request, CancellationToken cancellationToken)
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
        if(request.AddressId != 0)
        {
            var address = _context.Addresses.FirstOrDefault(e => e.Id == request.AddressId);
            if (address == null)
            {
                return new SuccessDto
                {
                    status = false,
                    message = $"Address With Id of {request.AddressId} Not Found"
                };
            }
        employee.Address = address;
        }

        if (request.JobId != 0)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);
            if(job == null)
            {
                return new SuccessDto
                {
                    status = false,
                    message = $"Job With Id of {request.JobId} Not Found"
                };
            }
            employee.Job = job;
        }
        employee.LastName = request.LastName;
        employee.FirstName = request.FirstName;

        await _context.SaveChangesAsync();
        return new SuccessDto
        {
            status = true,
            message = $"Operation Completed Successfully",
            data = new { Employees = employee }
        };
    }
}
