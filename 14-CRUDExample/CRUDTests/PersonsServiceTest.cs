﻿using Entities;
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

    public PersonsServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _personsService = new PersonsService();
        _countriesService = new CountriesService(false);
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

    [Fact]
    public void GetPersonByPersonId_NullPersonId()
    {
        // Arrange
        Guid? personId = null;

        // Act
        var personResponse = _personsService.GetPersonByPersonId(personId);

        // Assert
        Assert.Null(personResponse);
    }

    [Fact]
    public void GetPersonByPersonId_WithPersonId()
    {
        // Arrange
        var addedCountry = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "United States",
        });

        var addedPerson = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Elyn",
            Email = "Elyn@gmail.com",
            CountryId = addedCountry.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
            ReceiveNewsletter = true,
        });

        // Act
        var actual = _personsService.GetPersonByPersonId(addedPerson.PersonId);

        // Assert
        Assert.Equal(actual, addedPerson);
    }

    [Fact]
    public void GetAllPersons_EmptyList()
    {
        // Arrange

        // Act
        var actual = _personsService.GetAllPersons();


        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public void GetAllPersons_AddFewPersons()
    {
        // Arrange
        var usa = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = _personsService.AddPerson(new PersonAddRequest
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
        var actual = _personsService.GetAllPersons();

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        foreach (var person in addedPersons)
        {
            Assert.Contains(person, actual);
        }
    }

    [Fact]
    public void GetFilteredPersons_EmptySearchText()
    {
        // Arrange
        var usa = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = _personsService.AddPerson(new PersonAddRequest
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
        var actual = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        foreach (var person in addedPersons)
        {
            Assert.Contains(person, actual);
        }
    }

    [Fact]
    public void GetFilteredPersons_SearchByPersonName()
    {
        // Arrange
        var usa = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = _personsService.AddPerson(new PersonAddRequest
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
        var actual = _personsService.GetFilteredPersons(nameof(Person.PersonName), "Ma");

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        foreach (var person in addedPersons)
        {
            if (person.PersonName.Contains("Ma", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Contains(person, actual);
            }
        }
    }

    [Fact]
    public void GetSortedPersons()
    {
        // Arrange
        var usa = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "USA",
        });
        var canada = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "Canada",
        });

        var addedPersons = new List<PersonResponse>();
        var smith = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "Smith@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Male,
            ReceiveNewsletter = true,
        });
        addedPersons.Add(smith);

        var mary = _personsService.AddPerson(new PersonAddRequest
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
        var actual = _personsService
            .GetSortedPersons(_personsService.GetAllPersons(), nameof(Person.PersonName), SortOptions.Desc).ToList();

        _testOutputHelper.WriteLine("Actual:");
        foreach (var person in actual)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        for (var i = 0; i < addedPersons.Count; i++)
        {
            Assert.Equal(addedPersons[i], actual[i]);
        }
    }

    [Fact]
    public void UpdatePerson_NullPerson()
    {
        // Arrange
        PersonUpdateRequest personUpdateRequest = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            var actual = _personsService.UpdatePerson(personUpdateRequest);
        });
    }

    [Fact]
    public void UpdatePerson_InvalidPersonId()
    {
        // Arrange
        PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest
        {
            PersonId = Guid.Empty,
        };

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            var actual = _personsService.UpdatePerson(personUpdateRequest);
        });
    }

    [Fact]
    public void UpdatePerson_PersonNameIsNull()
    {
        // Arrange
        var uk = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "uk"
        });

        var addedPerson = _personsService.AddPerson(new PersonAddRequest
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
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personsService.UpdatePerson(updatedPerson);
        });
    }

    [Fact]
    public void UpdatePerson_PersonFullDetailsUpdation()
    {
        // Arrange
        var uk = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "uk"
        });

        var addedPerson = _personsService.AddPerson(new PersonAddRequest
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
        var actual = _personsService.UpdatePerson(updatedPerson);

        var person = _personsService.GetPersonByPersonId(updatedPerson.PersonId);

        // Assert
        Assert.Equal(actual, person);
    }

    [Fact]
    public void DeletePerson_ValidPersonId()
    {
        // Arrange
        var usa = _countriesService.AddCountry(new CountryAddRequest
        {
            CountryName = "usa"
        });

        var person = _personsService.AddPerson(new PersonAddRequest
        {
            PersonName = "ellen",
            Email = "ellen@gmail.com",
            CountryId = usa.CountryId,
            DateOfBirth = DateTime.Now,
            Gender = GenderOptions.Female,
        });

        // Act
        var isDeleted = _personsService.DeletePerson(person.PersonId);

        // Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public void DeletePerson_InvalidPersonId()
    {
        // Arrange

        // Act
        var isDeleted = _personsService.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.False(isDeleted);
    }
}