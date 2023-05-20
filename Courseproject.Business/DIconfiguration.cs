using Courseproject.Business.Services;
using Courseproject.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Courseproject.Business;

public class DIconfiguration
{
    public static void RegisterServices(IServiceCollection service)
    {
        service.AddAutoMapper(typeof(DtoEntityMapperProfile));
        service.AddScoped<IAddressService, AddressService>();
    }
}
