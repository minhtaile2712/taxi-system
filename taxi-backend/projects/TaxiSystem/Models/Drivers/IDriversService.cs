using TaxiSystem.Dtos;

namespace TaxiSystem.Models.Drivers;

public interface IDriversService
{
  Task<DriverDto> AddAsync(DriverCreateDto input);
  Task<List<DriverDto>> GetDriversAsync();
  Task<List<DriverDto>> FindNearbyDriversAsync(FindNearbyDriversDto input);
  Task<DriverDto?> GetByIdAsync(long id);
  Task<DriverDto?> UpdateDriverLocationAsync(long id, LocationDto location);
  Task<DriverDto?> DeleteByIdAsync(long id);
  Task<double[]> Test();
}
