using tutorial8.DTOs;
using tutorial8.Models;

namespace tutorial8.Mappers;

public static class ClientMapper
{
    public static ClientDto MapToClientDto(this Client client)
    {
        return new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName
        };
    }
}