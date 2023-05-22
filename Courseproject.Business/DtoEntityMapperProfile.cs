using AutoMapper;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Model;

namespace Courseproject.Business;

public class DtoEntityMapperProfile : Profile
{
	public DtoEntityMapperProfile()
	{
		CreateMap<AddressCreate, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<AddressUpdate, Address>();
		CreateMap<AddressGet, Address>();
	}

}