using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TaxiSystem.Dtos;

namespace TaxiSystem.Models;

public class CustomersService : ICustomersService
{
    private readonly TaxiSystemContext _context;

    public CustomersService(TaxiSystemContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto> AddAsync(CustomerCreateDto input)
    {
        var customer = new Customer(input.PhoneNumber);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        var result = MapCustomerToDto(customer);
        return result;
    }

    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        var result = customers.Select(MapCustomerToDto).ToList();
        return result;
    }

    public async Task<CustomerDto?> GetByIdAsync(long id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;
        return MapCustomerToDto(customer);
    }

    public async Task<CustomerDto?> UpdateCustomerLocationAsync(long id, LocationDto location)
    {
        CustomerDto? result = null;

        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            customer.Location = new Point(location.Long, location.Lat) { SRID = 4326 };
            await _context.SaveChangesAsync();
            result = MapCustomerToDto(customer);
        }

        return result;
    }

    public async Task<CustomerDto?> DeleteByIdAsync(long id)
    {
        CustomerDto? result = null;

        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            result = MapCustomerToDto(customer);
        }

        return result;
    }

    private static CustomerDto MapCustomerToDto(Customer d)
    {
        var result = new CustomerDto
        {
            Id = d.Id,
            PhoneNumber = d.PhoneNumber,
            Location = d.Location != null ? new()
            {
                Long = d.Location.X,
                Lat = d.Location.Y
            } : null
        };
        return result;
    }
}
