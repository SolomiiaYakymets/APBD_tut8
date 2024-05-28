using Microsoft.EntityFrameworkCore;
using tutorial8.Context;
using tutorial8.DTOs;
using tutorial8.Mappers;
using tutorial8.Models;

namespace tutorial8.Services;

public class TripService : ITripService
{
    private readonly ApbdTut8Context _dbContext;

    public TripService(ApbdTut8Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PageResult<TripDto>> GetPaginatedTrips(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 10) pageSize = 10;
        var tripsQuery = _dbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom);

        var tripsCount = await tripsQuery.CountAsync();
        var totalPages = (int)Math.Ceiling((double)tripsCount / pageSize);
        var trips = await _dbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var result =  new PageResult<Trip>
        {
            PageSize = pageSize,
            PageNum = page,
            AllPages = totalPages,
            Data = trips
        };

        var mappedTrips = new PageResult<TripDto>
        {
            AllPages = result.AllPages,
            Data = result.Data.Select(trip => trip.MapToGetTripDto()).ToList(),
            PageNum = result.PageNum,
            PageSize = result.PageSize
        };

        return mappedTrips;
    }

    public async Task<List<TripDto>> GetTrips()
    {
        var trips = await _dbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .ToListAsync();
        var result = trips.Select(trip => trip.MapToGetTripDto()).ToList();
        return result;
    }
}