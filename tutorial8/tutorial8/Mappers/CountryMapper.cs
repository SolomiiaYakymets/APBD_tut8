using tutorial8.DTOs;
using tutorial8.Models;

namespace tutorial8.Mappers;

public static class CountryMapper
{
    public static CountryDto MapToCountryDto(this Country country)
    {
        return new CountryDto
        {
            Name = country.Name
        };
    }
}