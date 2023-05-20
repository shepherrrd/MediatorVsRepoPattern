using Courseproject.Common.Dtos;
using Courseproject.Common.Model;

namespace Courseproject.Common.Interfaces;

public interface IAddressService
{
    Task<Address> CreateAddressAsync(AddressCreate addressCreate);
    Task UpdateAddressAsync(AddressUpdate addressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
    Task<Address> GetAddressAsync(int id);
    Task<List<Address>> GetAddressesAsync();
}
