using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class CountriesRepository(ApplicationDbContext db) : ICountriesRepository
{
    public async Task<Country> AddCountry(Country country)
    {
        await db.Countries.AddAsync(country);
        await db.SaveChangesAsync();
        return country;
    }

    public Task<List<Country>> GetAllCountries()
    {
        return db.Countries.ToListAsync();
    }

    public async Task<Country> GetCountryById(Guid countryId)
    {
        return await db.Countries.FindAsync(countryId);
    }

    public async Task<Country> GetCountryByName(string countryName)
    {
        return await db.Countries.FirstOrDefaultAsync(x => x.Name == countryName);
    }
}