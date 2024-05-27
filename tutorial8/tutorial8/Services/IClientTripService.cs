using Microsoft.AspNetCore.Mvc;
using tutorial8.DTOs;

namespace tutorial8.Services;

public interface IClientTripService
{
    Task AssignClientToTrip(ClientAssignDto request);
}