using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Courseproject.API.CQRS.EmployeeHandler.Queries;

public class GetAllEmployees : IRequest<SuccessDto>
{
    public string EmployeeName { get; set; } = string.Empty;
    public string Addressname { get; set; } = string.Empty;
    public string JobName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty; 
    
    public int Skip { get; set; } 

    public int Take { get; set; }
}

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployees, SuccessDto>
{
    private readonly ApplicationDbContext _context;
    public GetAllEmployeesHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SuccessDto> Handle(GetAllEmployees request, CancellationToken cancellationToken)
    {
        var employee = await (from e in _context.Employees
                              join a in _context.Addresses on e.Address.Id equals a.Id
                              join j in _context.Jobs on e.Job.Id equals j.Id
                              select new Employee
                              {
                                  Address = a,
                                  Id = e.Id,
                                  Job = j,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                              }).ToListAsync();
        var dto = new SuccessDto();

        if(!employee.Any())
        {
            dto.status = false;
            dto.message = " No Employees Found";
            return dto; 
        }
        if (!string.IsNullOrEmpty(request.EmployeeName)){
            employee = employee.Where(c => c.FirstName.Contains(request.EmployeeName) || c.LastName.Contains(request.EmployeeName)).ToList();
        }
        if (!string.IsNullOrEmpty(request.Addressname))
        {
            employee = employee.Where(a => a.Address.City.Contains(request.Addressname)).ToList();
        }
        if(!string.IsNullOrEmpty(request.TeamName))
        {
            employee = employee.Where(t => t.Teams.Any(l => l.Name.Contains(request.TeamName))).ToList();
        }
        if(request.Skip != 0)
        {
            employee = employee.Skip((request.Skip - 1) * request.Take).ToList();
        }
        if(request.Take!= 0)
        {
            employee = employee.Take(request.Take).ToList(); 
        }

        employee = employee.Select(employee => new Employee { Id = employee.Id, FirstName = employee.FirstName, Job = employee.Job,Address= employee.Address,
            LastName = employee.LastName, 
             }).ToList();
        var data = new[]
        {
            new
            {
                Employees = employee,
                NextPage = "?page=2"
            }
        };
        dto.data = data;
        return dto;

    }
}
