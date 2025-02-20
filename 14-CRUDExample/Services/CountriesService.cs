﻿using System.Reflection.Metadata.Ecma335;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    private readonly List<Country> _countries;

    public CountriesService(bool initialize = true)
    {
        _countries = new();

        if (initialize)
        {
            _countries = new[] { "Beijing", "Shanghai", "GuangZhou", "ShenZhen" }.Select(x => new Country
            {
                Id = Guid.NewGuid(),
                Name = x,
            }).ToList();
        }
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        if (countryAddRequest?.CountryName == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        if (_countries.Any(x => x.Name == countryAddRequest.CountryName))
            throw new ArgumentException($"Country {countryAddRequest.CountryName} already exists");

        var country = countryAddRequest.ToCountry();
        country.Id = Guid.NewGuid();
        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(x => x.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryId(Guid? countryId)
    {
        return _countries?.FirstOrDefault(x => x.Id == countryId)?.ToCountryResponse();
    }
}