using AutoMapper;
using Courseproject.Common.Dtos;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;

namespace Courseproject.Business.Services;

public class AddressService : IAddressService
{
    private IMapper Mapper { get; }
    public IGenericRepository<Address> AddressRepository { get; }

    public AddressService(IMapper mapper,IGenericRepository<Address> addressRepository)
    {
        Mapper = mapper;
        AddressRepository = addressRepository;
    }


    public async Task<Address> CreateAddressAsync(AddressCreate addressCreate)
    {
        var entity = Mapper.Map<Address>(addressCreate);
       await AddressRepository.InsertAsync(entity);
      await AddressRepository.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var address = await AddressRepository.GetByIdAsync(addressDelete.Id);
         AddressRepository.Delete(address);
           await AddressRepository.SaveChangesAsync();
    
    }

    public async Task<Address> GetAddressAsync(int id)
    {
        var address = await AddressRepository.GetByIdAsync(id);
        return address;
    }

    public async Task<List<Address>> GetAddressesAsync()
    {
        var address = await AddressRepository.GetAsync(null, null);
        return address;
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        var address = Mapper.Map<Address>(addressUpdate);
         AddressRepository.Update(address);
        await AddressRepository.SaveChangesAsync();
    }
}
