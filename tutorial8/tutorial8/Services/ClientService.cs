using Microsoft.EntityFrameworkCore;
using tutorial8.Context;

namespace tutorial8.Services;

public class ClientService : IClientService
{
    private readonly ApbdTut8Context _dbContext;

    public ClientService(ApbdTut8Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> HasAssignedTrips(int clientId)
    {
        return await _dbContext.ClientTrips.AnyAsync(ct => ct.IdClient == clientId);
    }

    public async Task<bool> DeleteClient(int clientId)
    {
        var client = await _dbContext.Clients.FindAsync(clientId);

        if (client == null)
        {
            return false;
        }

        try
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}