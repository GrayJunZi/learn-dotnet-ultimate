using System.Reflection.Metadata.Ecma335;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService(ICountriesRepository countriesRepository) : ICountriesService
{
    public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
    {
        if (countryAddRequest?.CountryName == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        if (await countriesRepository.GetCountryByName(countryAddRequest.CountryName) != null)
            throw new ArgumentException($"Country {countryAddRequest.CountryName} already exists");

        var country = countryAddRequest.ToCountry();
        country.Id = Guid.NewGuid();
        await countriesRepository.AddCountry(country);
        return country.ToCountryResponse();
    }

    public async Task<List<CountryResponse>> GetAllCountries()
    {
        return (await countriesRepository.GetAllCountries())
            .Select(x => x.ToCountryResponse())
            .ToList();
    }

    public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryId)
    {
        return countryId.HasValue
            ? (await countriesRepository.GetCountryById(countryId.Value))?.ToCountryResponse()
            : null;
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
                if (await countriesRepository.GetCountryByName(cellValue) == null)
                {
                    await countriesRepository.AddCountry(new Country
                    {
                        Name = cellValue,
                    });
                    
                    inserted++;
                }
            }
        }

        return inserted;
    }
}