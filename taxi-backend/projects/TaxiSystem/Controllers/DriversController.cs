using Microsoft.AspNetCore.Mvc;
using TaxiSystem.Dtos;
using TaxiSystem.Dtos.Drivers;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly IDriversService _driversService;

    public DriversController(IDriversService driversService)
    {
        _driversService = driversService;
    }

    /// <summary>
    /// Add driver
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<DriverDto>> PostDriver(DriverCreateDto input)
    {
        var result = await _driversService.AddAsync(input);
        return CreatedAtAction(nameof(PostDriver), new { id = result.Id }, result);
    }

    /// <summary>
    /// Get drivers
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
    {
        var result = await _driversService.GetDriversAsync();
        return result;
    }

    /// <summary>
    /// Find nearby drivers
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("nearby")]
    public async Task<ActionResult<List<DriverDto>>> FindNearbyDrivers([FromQuery] FindNearbyDriversDto input)
    {
        var result = await _driversService.FindNearbyDriversAsync(input);
        return result;
    }

    /// <summary>
    /// Get driver by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetDriverById(long id)
    {
        var result = await _driversService.GetByIdAsync(id);
        return (result == null) ? NotFound() : result;
    }

    /// <summary>
    /// Update driver
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateDriver(long id, DriverUpdateDto input)
    {
        var result = await _driversService.UpdateDriverAsync(id, input);
        return (result == null) ? NotFound() : NoContent();
    }

    /// <summary>
    /// Delete driver by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriverById(long id)
    {
        var result = await _driversService.DeleteByIdAsync(id);
        return (result == null) ? NotFound() : NoContent();
    }

    /// <summary>
    /// Update driver location
    /// </summary>
    /// <param name="id"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    [HttpPost("{id}/location")]
    public async Task<IActionResult> UpdateDriverLocation(long id, LocationDto location)
    {
        var result = await _driversService.UpdateDriverLocationAsync(id, location);
        return (result == null) ? NotFound() : NoContent();
    }

    /// <summary>
    /// Get driver by phone number
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    [HttpGet("by-phone/{phoneNumber}")]
    public async Task<DriverDto?> GetDriverByPhone(string phoneNumber)
    {
        var result = await _driversService.GetDriverByPhoneAsync(phoneNumber);
        return result;
    }

    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    [HttpGet("test")]
    public async Task<double[]> Test()
    {
        var result = await _driversService.Test();
        return result;
    }
}
