using Microsoft.AspNetCore.Mvc;
using tutorial8.DTOs;
using tutorial8.Services;

namespace tutorial8.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientService _clientService;
    private readonly IClientTripService _clientTripService;

    public TripController(ITripService tripService, IClientService clientService, IClientTripService clientTripService)
    {
        _tripService = tripService;
        _clientService = clientService;
        _clientTripService = clientTripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int? page,[FromQuery] int? pageSize)
    {
        if (page is null || pageSize is null)
            return Ok(await _tripService.GetTrips());

        return Ok(await _tripService.GetPaginatedTrips(page.Value, pageSize.Value));
    }

    [HttpDelete("clients/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var hasAssignedTrips = await _clientService.HasAssignedTrips(idClient);
        if (hasAssignedTrips)
        {
            return BadRequest("Cannot delete client because they have assigned trips.");
        }
        
        var deleted = await _clientService.DeleteClient(idClient);
        if (deleted)
        {
            return Ok("Client deleted.");
        }
        return NotFound("Client not found or deletion failed.");
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientAssignDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.IdTrip != idTrip)
        {
            return BadRequest("Trip ID mismatch.");
        }

        try
        {
            await _clientTripService.AssignClientToTrip(request);
            return Ok("Client assigned to trip.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}