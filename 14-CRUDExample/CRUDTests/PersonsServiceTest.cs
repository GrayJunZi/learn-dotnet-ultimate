using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUDTests;

public class PersonsServiceTest
{
    private readonly IPersonsService _personsService;

    public PersonsServiceTest()
    {
        _personsService = new PersonsService();
    }

    [Fact]
    public void AddPerson_NullPerson()
    {
        // Arrange
        PersonAddRequest? personAddRequest = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            var actual = _personsService.AddPerson(personAddRequest);
        });
    }

    [Fact]
    public void AddPerson_PersonNameIsNull()
    {
        // Arrange
        PersonAddRequest? personAddRequest = new PersonAddRequest
        {
            PersonName = null
        };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            var actual = _personsService.AddPerson(personAddRequest);
        });
    }

    [Fact]
    public void AddPerson_ProperPersonDetails()
    {
        // Arrange
        PersonAddRequest? personAddRequest = new PersonAddRequest
        {
            PersonName = "hudson",
            DateOfBirth = DateTime.Now,
            Email = "hudson@gmail.com",
            Address = "Hudson Road",
            CountryId = Guid.NewGuid(),
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        };

        // Act
        var addedPerson = _personsService.AddPerson(personAddRequest);
        var allPersons = _personsService.GetAllPersons();

        // Assert
        Assert.True(addedPerson.PersonId != Guid.Empty);
        Assert.Contains(addedPerson, allPersons);
    }
}