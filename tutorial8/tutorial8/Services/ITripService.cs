using tutorial8.DTOs;
using tutorial8.Models;

namespace tutorial8.Services;

public interface ITripService
{
    Task<PageResult<TripDto>> GetPaginatedTrips(int page = 1, int pageSize = 10);
    Task<List<TripDto>> GetTrips();
}