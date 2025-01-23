using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null )
            return false;
        
        if (obj is not CountryResponse countryResponse)
            return false;
        
        return this.CountryId == countryResponse.CountryId && this.CountryName == countryResponse.CountryName;
    }
}

public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse
        {
            CountryId = country.Id,
            CountryName = country.Name
        };
    }
}