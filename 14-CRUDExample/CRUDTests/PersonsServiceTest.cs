using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDTests;

public class PersonsServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;
    private readonly IFixture _fixture;

    public PersonsServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _countriesService =
            new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
        _personsService =
            new PersonsService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options),
                _countriesService);

        _fixture = new Fixture();
    }

    [Fact]
    public async Task AddPerson_NullPerson()
    {
        // Arrange
        PersonAddRequest? personAddRequest = null;

        // Assert
        /*
        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            // Act
            var actual = await _personsService.AddPerson(personAddRequest);
        });
        */

        Func<Task> action = async () => await _personsService.AddPerson(personAddRequest);
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddPerson_PersonNameIsNull()
    {
        // Arrange
        PersonAddRequest? personAddRequest = _fixture
            .Build<PersonAddRequest>()
            .With(x => x.PersonName, null as string)
            .Create();

        // Assert
        /*
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            var actual = await _personsService.AddPerson(personAddRequest);
        });
        */

        Func<Task> action = async () => await _personsService.AddPerson(personAddRequest);
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task AddPerson_ProperPersonDetails()
    {
        // Arrange
        /*
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
        */

        var personAddRequest = _fixture
            .Build<PersonAddRequest>()
            .With(x => x.Email,"example@test.com")
            .Create();

        // Act
        var addedPerson = await _personsService.AddPerson(personAddRequest);
        var allPersons = await _personsService.GetAllPersons();

        // Assert
        // Assert.True(addedPerson.PersonId != Guid.Empty);
        // Assert.Contains(addedPerson, allPersons);

        addedPerson.PersonId.Should().NotBe(Guid.Empty);
        allPersons.Should().Contain(addedPerson);
    }

    [Fact]
    public async Task GetPersonByPersonId_NullPersonId()
    {
        // Arrange
        Guid? personId = null;

        // Act
        var personResponse = await _personsService.GetPersonByPersonId(personId);

        // Assert
        // Assert.Null(personResponse);
        personResponse.Should().BeNull();
    }

    [Fact]
    public async Task GetPersonByPersonId_WithPersonId()
    {
        // Arrange
        var addedCountry = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "United States",
        });

        var addedPerson = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Elyn",
            Email = "Elyn@gmail.com",
            CountryId = addedCountry.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });

        // Act
        var actual = await _personsService.GetPersonByPersonId(addedPerson.PersonId);

        // Assert
        // Assert.Equal(actual, addedPerson);
        actual.Should().Be(addedPerson);
    }

    [Fact]
    public async Task GetAllPersons_EmptyList()
    {
        // Arrange

        // Act
        var actual = await _personsService.GetAllPersons();


        // Assert
        // Assert.Empty(actual);
        actual.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllPersons_AddFewPersons()
    {
        // Arrange
        var usa = await _countriesService.AddCountry(_fixture.Create<CountryAddRequest>());
        var canada = await _countriesService.AddCountry(_fixture.Create<CountryAddRequest>());

        var addedPersons = new List<PersonResponse>();
        var smith = await _personsService.AddPerson(_fixture
            .Build<PersonAddRequest>()
            .With(x => x.Email, "smith@gmail.com")
            .Create());
        addedPersons.Add(smith);

        var mary = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "Mary@gmail.com",
            CountryId = canada.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(mary);

        _testOutputHelper.WriteLine($"Expected:");
        foreach (var person in addedPersons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Act
        var actual = await _personsService.GetAllPersons();

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        /*
        foreach (var person in addedPersons)
        {
            Assert.Contains(person, actual);
        }
        */

        actual.Should().BeEquivalentTo(addedPersons);
    }

    [Fact]
    public async Task GetFilteredPersons_EmptySearchText()
    {
        // Arrange
        var usa = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "Mary@gmail.com",
            CountryId = canada.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(mary);

        _testOutputHelper.WriteLine($"Expected:");
        foreach (var person in addedPersons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Act
        var actual = await _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        /*
        foreach (var person in addedPersons)
        {
            Assert.Contains(person, actual);
        }
        */

        actual.Should().BeEquivalentTo(addedPersons);
    }

    [Fact]
    public async Task GetFilteredPersons_SearchByPersonName()
    {
        // Arrange
        var usa = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "Mary@gmail.com",
            CountryId = canada.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(mary);

        _testOutputHelper.WriteLine($"Expected:");
        foreach (var person in addedPersons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Act
        var actual = await _personsService.GetFilteredPersons(nameof(Person.PersonName), "Ma");

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        /*
        foreach (var person in addedPersons)
        {
            if (person.PersonName.Contains("Ma", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Contains(person, actual);
            }
        }
        */

        actual.Should().OnlyContain(temp => temp.PersonName.Contains("Ma", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task GetSortedPersons()
    {
        // Arrange
        var usa = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "Mary@gmail.com",
            CountryId = canada.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(mary);
        addedPersons = addedPersons.OrderByDescending(p => p.PersonName).ToList();

        _testOutputHelper.WriteLine($"Expected:");
        foreach (var person in addedPersons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Act
        var actual = await _personsService
            .GetSortedPersons(await _personsService.GetAllPersons(), nameof(Person.PersonName), SortOptions.Desc);

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        /*
        for (var i = 0; i < addedPersons.Count; i++)
        {
            Assert.Equal(addedPersons[i], actual[i]);
        }
        */
        actual.Should().BeInDescendingOrder(temp => temp.PersonName);
    }

    [Fact]
    public async Task UpdatePerson_NullPerson()
    {
        // Arrange
        PersonUpdateRequest personUpdateRequest = null;

        // Assert
        /*
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            var actual = await _personsService.UpdatePerson(personUpdateRequest);
        });
        */

        Func<Task> action = async () => await _personsService.UpdatePerson(personUpdateRequest);
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdatePerson_InvalidPersonId()
    {
        // Arrange
        PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest
        {
            PersonId = Guid.Empty,
        };

        // Assert
        /*
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            var actual = await _personsService.UpdatePerson(personUpdateRequest);
        });
        */
        Func<Task> action = async () => await _personsService.UpdatePerson(personUpdateRequest);
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdatePerson_PersonNameIsNull()
    {
        // Arrange
        var uk = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "uk"
        });

        var addedPerson = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "John",
            Email = "John@gmail.com",
            Gender = GenderOptions.Male,
            CountryId = uk.CountryId,
            DateOfBirth = DateTime.Now,
        });

        var updatedPerson = addedPerson.ToPersonUpdateRequest();
        updatedPerson.PersonName = null;

        // Assert
        /*
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _personsService.UpdatePerson(updatedPerson);
        });
        */
        
        Func<Task> action = async () => await _personsService.UpdatePerson(updatedPerson);
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task UpdatePerson_PersonFullDetailsUpdation()
    {
        // Arrange
        var uk = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "uk"
        });

        var addedPerson = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "John",
            Email = "John@gmail.com",
            CountryId = uk.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
        });

        var updatedPerson = addedPerson.ToPersonUpdateRequest();
        updatedPerson.PersonName = "William";
        updatedPerson.Email = "William@gmail.com";

        // Act
        var actual = await _personsService.UpdatePerson(updatedPerson);

        var person = await _personsService.GetPersonByPersonId(updatedPerson.PersonId);

        // Assert
        // Assert.Equal(actual, person);

        actual.Should().Be(person);
    }

    [Fact]
    public async Task DeletePerson_ValidPersonId()
    {
        // Arrange
        var usa = await _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "usa"
        });

        var person = await _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "ellen",
            Email = "ellen@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
        });

        // Act
        var isDeleted = await _personsService.DeletePerson(person.PersonId);

        // Assert
        // Assert.True(isDeleted);
        isDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeletePerson_InvalidPersonId()
    {
        // Arrange

        // Act
        var isDeleted = await _personsService.DeletePerson(Guid.NewGuid());

        // Assert
        // Assert.False(isDeleted);
        isDeleted.Should().BeFalse();
    }
}