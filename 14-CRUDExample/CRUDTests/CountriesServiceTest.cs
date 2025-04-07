using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountriesService _countriesService;

    public CountriesServiceTest(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    // 当 CountryAddRequest 为空，则应该抛异常
    [Fact]
    public void AddCountry_NullCountry()
    {
        // Arrange
        CountryAddRequest? addRequest = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _countriesService.AddCountry(addRequest);
        });
    }

    // 当 CountryName 重复，则应该抛异常
    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        // Arrange
        CountryAddRequest? addRequest = new CountryAddRequest
        {
            CountryName = "China"
        };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countriesService.AddCountry(addRequest);
            _countriesService.AddCountry(addRequest);
        });
    }

    // 当添加成功后Id应该有值
    [Fact]
    public void AddCountry_ProperCountryDetail()
    {
        // Arrange
        CountryAddRequest? addRequest = new CountryAddRequest
        {
            CountryName = "China"
        };

        // Act
        var actual = _countriesService.AddCountry(addRequest);
        var allCountries = _countriesService.GetAllCountries();

        // Assert
        Assert.True(actual.CountryId != Guid.Empty);
        Assert.Contains(actual, allCountries);
    }

    // 当数据为空
    [Fact]
    public void GetAllCountries_EmptyList()
    {
        // Act
        var actual = _countriesService.GetAllCountries();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public void GetAllCountries_AddFewCountries()
    {
        // Arrange
        var countries = new List<CountryAddRequest>
        {
            new CountryAddRequest
            {
                CountryName = "China"
            },
            new CountryAddRequest
            {
                CountryName = "Russia"
            }
        };

        // Act
        var addedCountries = new List<CountryResponse>();
        foreach (var country in countries)
        {
            addedCountries.Add(_countriesService.AddCountry(country));
            ;
        }

        var actual = _countriesService.GetAllCountries();

        // Assert
        foreach (var expected in addedCountries)
        {
            Assert.Contains(expected, actual);
        }
    }

    [Fact]
    public void GetCountryByCountryId_NullCountry()
    {
        // Arrange
        Guid? countryId = null;

        // Act
        var actual = _countriesService.GetCountryByCountryId(countryId);

        //Assert
        Assert.Null(actual);
    }

    [Fact]
    public void GetCountryByCountryId_ValidCountry()
    {
        // Arrange
        var request = new CountryAddRequest
        {
            CountryName = "France"
        };
        var addedCountry = _countriesService.AddCountry(request);

        // Act
        var actual = _countriesService.GetCountryByCountryId(addedCountry.CountryId);
        
        // Assert
        Assert.Equal(addedCountry, actual);
    }
}