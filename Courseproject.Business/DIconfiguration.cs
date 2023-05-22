using Courseproject.Business.Services;
using Courseproject.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Courseproject.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
}
