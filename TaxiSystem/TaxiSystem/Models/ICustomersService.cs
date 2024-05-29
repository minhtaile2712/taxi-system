using TaxiSystem.Dtos;

namespace TaxiSystem.Models;

public interface ICustomersService
{
    Task<CustomerDto> AddAsync(CustomerCreateDto input);
    Task<List<CustomerDto>> GetCustomersAsync();
    Task<CustomerDto?> GetByIdAsync(long id);
    Task<CustomerDto?> UpdateCustomerLocationAsync(long id, LocationDto location);
    Task<CustomerDto?> DeleteByIdAsync(long id);
}
