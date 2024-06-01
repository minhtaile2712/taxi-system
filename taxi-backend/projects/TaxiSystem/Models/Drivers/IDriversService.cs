using TaxiSystem.Dtos;
using TaxiSystem.Dtos.Drivers;

namespace TaxiSystem.Models.Drivers;

public interface IDriversService
{
    Task<DriverDto> AddAsync(DriverCreateDto input);
    Task<List<DriverDto>> GetDriversAsync();
    Task<DriverDto?> GetByIdAsync(long id);
    Task<DriverDto?> UpdateDriverAsync(long id, DriverUpdateDto input);
    Task<DriverDto?> DeleteByIdAsync(long id);

    Task<List<DriverDto>> FindNearbyDriversAsync(FindNearbyDriversDto input);
    Task<DriverDto?> UpdateDriverLocationAsync(long id, LocationDto location);
    Task<double[]> Test();

    Task<List<Driver>> GetNearbyDriversAsync(double centerPointLong, double centerPointLat, double distanceInMeters);
}
