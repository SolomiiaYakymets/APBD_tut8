using Microsoft.EntityFrameworkCore;
using tutorial8.Context;
using tutorial8.DTOs;
using tutorial8.Models;

namespace tutorial8.Services;

public class ClientTripService : IClientTripService
{
    private readonly ApbdTut8Context _dbContext;

    public ClientTripService(ApbdTut8Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AssignClientToTrip(ClientAssignDto request)
    {
        var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Pesel == request.Pesel);
        if (existingClient != null)
        {
            throw new Exception("Client with the provided PESEL already exists.");
        }
        
        var trip = await _dbContext.Trips.FindAsync(request.IdTrip);
        if (trip == null || trip.DateFrom <= DateTime.Now)
        {
            throw new Exception("Invalid trip or trip date has passed.");
        }
        
        var existingClientTrip = await _dbContext.ClientTrips
            .FirstOrDefaultAsync(ct => ct.IdClientNavigation.Pesel == request.Pesel && ct.IdTrip == request.IdTrip);
        if (existingClientTrip != null)
        {
            throw new Exception("Client is already registered for the given trip.");
        }
        
        var client = new Client
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Telephone = request.Telephone,
            Pesel = request.Pesel
        };

        var clientTrip = new ClientTrip
        {
            IdClientNavigation = client,
            IdTrip = request.IdTrip,
            PaymentDate = request.PaymentDate,
            RegisteredAt = DateTime.Now
        };

        _dbContext.ClientTrips.Add(clientTrip);
        await _dbContext.SaveChangesAsync();
    }
}