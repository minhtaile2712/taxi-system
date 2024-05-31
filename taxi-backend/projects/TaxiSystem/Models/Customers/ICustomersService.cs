using TaxiSystem.Dtos;
using TaxiSystem.Dtos.Customers;

namespace TaxiSystem.Models.Customers;

public interface ICustomersService
{
    Task<CustomerDto> AddAsync(CustomerCreateDto input);
    Task<List<CustomerDto>> GetCustomersAsync();
    Task<CustomerDto?> GetByIdAsync(long id);
    Task<CustomerDto?> UpdateCustomerLocationAsync(long id, LocationDto location);
    Task<CustomerDto?> DeleteByIdAsync(long id);
    Task<CustomerDto?> GetCustomerByPhoneAsync(string phoneNumber);
}
