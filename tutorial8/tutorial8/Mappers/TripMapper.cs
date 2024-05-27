using tutorial8.DTOs;
using tutorial8.Models;

namespace tutorial8.Mappers;

public static class TripMapper
{
    public static TripDto MapToGetTripDto(this Trip trip)
    {
        return new TripDto
        {
            Name = trip.Name,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            Description = trip.Description,
            MaxPeople = trip.MaxPeople,
            Countries = trip.IdCountries.Select(country => country.MapToCountryDto()).ToList(),
            Clients = trip.ClientTrips.Select(e => e.IdClientNavigation.MapToClientDto()).ToList()
        };
    }
}