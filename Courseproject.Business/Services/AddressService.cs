using AutoMapper;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;

namespace Courseproject.Business.Services;

public class AddressService : IAddressService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Address> AddressRepository { get; }

    public AddressService(IMapper mapper, IGenericRepository<Address> addressRepository)
    {
        Mapper = mapper;
        AddressRepository = addressRepository;
    }


    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        var entity = Mapper.Map<Address>(addressCreate);
        await AddressRepository.InsertAsync(entity);
        await AddressRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await AddressRepository.GetByIdAsync(addressDelete.Id);
        AddressRepository.Delete(entity);
        await AddressRepository.SaveChangesAsync();
    }

    public async Task<AddressGet> GetAddressAsync(int id)
    {
        var entity = await AddressRepository.GetByIdAsync(id);
        return Mapper.Map<AddressGet>(entity);
    }

    public async Task<List<AddressGet>> GetAddressesAsync()
    {
        var entities = await AddressRepository.GetAsync(null, null);
        return Mapper.Map<List<AddressGet>>(entities);
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        var entity = Mapper.Map<Address>(addressUpdate);
        AddressRepository.Update(entity);
        await AddressRepository.SaveChangesAsync();
    }
}
