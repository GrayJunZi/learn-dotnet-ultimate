using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountriesService _countriesService;

    public CountriesServiceTest()
    {
        _countriesService = new CountriesService();
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

        // Assert
        Assert.True(actual.CountryId != Guid.Empty);
    }
}