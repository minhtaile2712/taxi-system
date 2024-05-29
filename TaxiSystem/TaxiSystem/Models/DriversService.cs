﻿using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TaxiSystem.Dtos;
using TaxiSystem.Extensions;

namespace TaxiSystem.Models;

public class DriversService : IDriversService
{
    private readonly TaxiSystemContext _context;

    public DriversService(TaxiSystemContext context)
    {
        _context = context;
    }

    public async Task<DriverDto> AddAsync(DriverCreateDto input)
    {
        var driver = new Driver(input.PhoneNumber);
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        var result = MapDriverToDto(driver);
        return result;
    }

    public async Task<List<DriverDto>> GetDriversAsync()
    {
        var drivers = await _context.Drivers.ToListAsync();
        var result = drivers.Select(MapDriverToDto).ToList();
        return result;
    }

    public async Task<List<DriverDto>> FindNearbyDriversAsync(FindNearbyDriversDto input)
    {
        var centerPoint = new Point(input.Long, input.Lat) { SRID = 4326 };
        var distanceInDegrees = MetersToDegrees(input.DistanceInMeters);

        var nearbyDrivers = await _context.Drivers
            .Where(d => d.Location != null && d.Location.Distance(centerPoint) < distanceInDegrees)
            .OrderBy(d => d.Location!.Distance(centerPoint))
            .ToListAsync();

        var result = nearbyDrivers.Select(MapDriverToDto).ToList();
        return result;
    }

    public async Task<DriverDto?> GetByIdAsync(long id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null) return null;
        return MapDriverToDto(driver);
    }

    public async Task<DriverDto?> UpdateDriverLocationAsync(long id, LocationDto location)
    {
        DriverDto? result = null;

        var driver = await _context.Drivers.FindAsync(id);
        if (driver != null)
        {
            driver.Location = new Point(location.Long, location.Lat) { SRID = 4326 };
            await _context.SaveChangesAsync();
            result = MapDriverToDto(driver);
        }

        return result;
    }

    public async Task<DriverDto?> DeleteByIdAsync(long id)
    {
        DriverDto? result = null;

        var driver = await _context.Drivers.FindAsync(id);
        if (driver != null)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            result = MapDriverToDto(driver);
        }

        return result;
    }

    public Task<double[]> Test()
    {
        var pointA = new Point(106.64964908560823, 10.80184768485948) { SRID = 4326 };
        var pointB = new Point(106.65010006612324, 10.801803284995989) { SRID = 4326 };
        var distanceInDegrees = pointA.Distance(pointB);
        var distanceInMeters = pointA.ProjectTo(32648).Distance(pointB.ProjectTo(32648));

        var result = new double[] { distanceInDegrees, distanceInMeters };
        return Task.FromResult(result);
    }

    private static double MetersToDegrees(double meters)
    {
        double degreesPerMeter = 1.0 / 111320.0;
        return meters * degreesPerMeter;
    }

    private static DriverDto MapDriverToDto(Driver d)
    {
        var result = new DriverDto
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
