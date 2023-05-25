
using Courseproject.API.CQRS.AddressHandler.Queries;
using Courseproject.API.CQRS.JobHandler.Queries;
using Courseproject.API.ResponseDtos;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courseproject.API.CQRS.EmployeeHandler.Command
{
    public class CreateEmployee : IRequest<SuccessDto>
    {
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public int AddressId { get; set; }
        public int JobId { get; set; }
    }

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, SuccessDto>
    {
       
        private readonly ApplicationDbContext _context;
        public CreateEmployeeHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessDto> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var dto = new SuccessDto(); 
            var address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == request.AddressId);
            var job = await _context.Jobs.FirstOrDefaultAsync(c => c.Id == request.JobId);
            if (address == null)
            {
                dto.status = false;
                dto.message = $"Address of Id {request.AddressId} Not Found ";
                return dto;
            }
            

            if (job == null)
            {
                dto.status = false;
                dto.message = $"Job of Id {request.JobId} Not Found ";
                return dto;
            }
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Job = job,
                Address = address,
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync(cancellationToken);

            dto.data = employee;
            
            return dto;

        }
    }
}
