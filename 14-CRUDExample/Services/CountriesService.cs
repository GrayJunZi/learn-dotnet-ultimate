using System.Reflection.Metadata.Ecma335;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    private readonly PersonsDbContext _db;

    public CountriesService(PersonsDbContext db)
    {
        _db = db;
    }

    public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
    {
        if (countryAddRequest?.CountryName == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        if (_db.Countries.Any(x => x.Name == countryAddRequest.CountryName))
            throw new ArgumentException($"Country {countryAddRequest.CountryName} already exists");

        var country = countryAddRequest.ToCountry();
        country.Id = Guid.NewGuid();
        _db.Countries.Add(country);
        await _db.SaveChangesAsync();

        return country.ToCountryResponse();
    }

    public async Task<List<CountryResponse>> GetAllCountries()
    {
        return await _db.Countries.Select(x => x.ToCountryResponse()).ToListAsync();
    }

    public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryId)
    {
        return (await _db.Countries?.FirstOrDefaultAsync(x => x.Id == countryId))?.ToCountryResponse();
    }

    public async Task<int> UploadCountriesFromExcelFile(IFormFile file)
    {
        var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var inserted = 0;

        using var excelPackage = new ExcelPackage(ms);
        var worksheet = excelPackage.Workbook.Worksheets.Add("Countries");

        var rows = worksheet.Dimension.Rows;

        for (var row = 2; row <= rows; row++)
        {
            var cellValue = worksheet.Cells[row, 1].Value?.ToString();

            if (!string.IsNullOrEmpty(cellValue))
            {
                if (!await _db.Countries.AnyAsync(x => x.Name == cellValue))
                {
                    _db.Countries.Add(new Country
                    {
                        Name = cellValue,
                    });
                    await _db.SaveChangesAsync();

                    inserted++;
                }
            }
        }

        return inserted;
    }
}