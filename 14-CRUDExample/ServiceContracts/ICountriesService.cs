﻿using Microsoft.AspNetCore.Http;
using ServiceContracts.DTO;

namespace ServiceContracts;

public interface ICountriesService
{
    Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    Task<List<CountryResponse>> GetAllCountries();
    Task<CountryResponse?> GetCountryByCountryId(Guid? countryId);
    Task<int> UploadCountriesFromExcelFile(IFormFile file);
}