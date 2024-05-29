using Microsoft.AspNetCore.Mvc;
using TaxiSystem.Dtos;
using TaxiSystem.Dtos.Customers;
using TaxiSystem.Models.Customers;

namespace TaxiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    /// <summary>
    /// Add customer
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerCreateDto input)
    {
        var result = await _customersService.AddAsync(input);
        return CreatedAtAction(nameof(PostCustomer), new { id = result.Id }, result);
    }

    /// <summary>
    /// Get customers
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var result = await _customersService.GetCustomersAsync();
        return result;
    }


    /// <summary>
    /// Get customer by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(long id)
    {
        var result = await _customersService.GetByIdAsync(id);
        return (result == null) ? NotFound() : result;
    }

    /// <summary>
    /// Update customer location
    /// </summary>
    /// <param name="id"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    [HttpPost("{id}/location")]
    public async Task<IActionResult> UpdateCustomerLocation(long id, LocationDto location)
    {
        var result = await _customersService.UpdateCustomerLocationAsync(id, location);
        return (result == null) ? NotFound() : NoContent();
    }

    /// <summary>
    /// Delete customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(long id)
    {
        var result = await _customersService.DeleteByIdAsync(id);
        return (result == null) ? NotFound() : NoContent();
    }
}
