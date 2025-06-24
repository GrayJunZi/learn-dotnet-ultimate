using Entities;
using RepositoryContracts;

namespace Repositories;

public class PersonsRepository : ICountriesRepository
{
    public Task<Country> AddCountry(Country country)
    {
        throw new NotImplementedException();
    }

    public Task<List<Country>> GetAllCountries()
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetCountryById(Guid countryId)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetCountryByName(string countryName)
    {
        throw new NotImplementedException();
    }
}