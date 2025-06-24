using AutoFixture;
using CRUDExample.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDTests;

public class PersonsControllerTest
{
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;

    private readonly Mock<IPersonsService> _personsServiceMock;
    private readonly Mock<ICountriesService> _countriesServiceMock;

    private readonly Fixture _fixture;

    public PersonsControllerTest()
    {
        _fixture = new Fixture();

        _personsServiceMock = new Mock<IPersonsService>();
        _countriesServiceMock = new Mock<ICountriesService>();

        _personsService = _personsServiceMock.Object;
        _countriesService = _countriesServiceMock.Object;
    }

    #region Index

    [Fact]
    public async Task Index_ShouldReturnIndexViewWithPersonsList()
    {
        var person_response_list = _fixture.Create<List<PersonResponse>>();

        var personsController = new PersonsController(_personsService, _countriesService);

        _personsServiceMock
            .Setup(x => x.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(person_response_list);

        _personsServiceMock
            .Setup(x => x.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(),
                It.IsAny<SortOptions>()))
            .ReturnsAsync(person_response_list);

        // Act
        IActionResult result = await personsController.Index(_fixture.Create<string>(), _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<SortOptions>());

        // Assert
        ViewResult viewResult = Assert.IsType<ViewResult>(result);

        viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
        viewResult.ViewData.Model.Should().Be(person_response_list);
    }

    #endregion
    
    #region Create

    [Fact]
    public async Task Create_IfModelErrors_ToReturnCreateView()
    {
        // Arrange
        PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();
        
        PersonResponse person_response = _fixture.Create<PersonResponse>();
        
        List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

        _countriesServiceMock.Setup(x => x.GetAllCountries())
            .ReturnsAsync(countries);

        _personsServiceMock.Setup(x => x.AddPerson(It.IsAny<PersonAddRequest>()))
            .ReturnsAsync(person_response);
        
        PersonsController personsController = new PersonsController(_personsService, _countriesService);
        
        // Act
        personsController.ModelState.AddModelError("PersonName", "Person Name can't be blank");

        IActionResult result = await personsController.Create(person_add_request);
        
        // Assert
        ViewResult viewResult = Assert.IsType<ViewResult>(result);
        
        viewResult.ViewData.Model.Should().BeAssignableTo<PersonAddRequest>();
        viewResult.ViewData.Model.Should().Be(person_add_request);
    }
    
    #endregion
}