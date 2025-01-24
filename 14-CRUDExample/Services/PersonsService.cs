using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonsService : IPersonsService
{
    private readonly List<Person> _persons = new();
    private readonly ICountriesService _countriesService = new CountriesService();

    public PersonResponse? AddPerson(PersonAddRequest? personAddRequest)
    {
        if (personAddRequest == null)
            throw new ArgumentNullException(nameof(personAddRequest));

        ValidationHelper.ModelValidation(personAddRequest);

        var person = personAddRequest.ToPerson();

        person.PersonId = Guid.NewGuid();
        _persons.Add(person);

        return convertPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        return _persons.Select(convertPersonResponse).ToList();
    }


    private PersonResponse? convertPersonResponse(Person person)
    {
        var personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
        return personResponse;
    }


    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == null)
            return null;
        return _persons?.FirstOrDefault(p => p.PersonId == personId)?.ToPersonResponse();
    }


    public List<PersonResponse> GetFilteredPersons(string field, string? search)
    {
        var persons = GetAllPersons();

        if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(search))
            return persons;

        switch (field)
        {
            case nameof(Person.PersonName):
                return persons.Where(p => p.PersonName.Contains(search)).ToList();
            case nameof(Person.Email):
                return persons.Where(p => p.Email.Contains(search)).ToList();
            case nameof(Person.Gender):
                return persons.Where(p => p.Gender == search).ToList();
            case nameof(Person.Address):
                return persons.Where(p => p.Address.Contains(search)).ToList();
            default:
                return persons;
        }
    }

    public List<PersonResponse> GetSortedPersons(List<PersonResponse> persons, string? field, SortOptions sortOptions)
    {
        if (string.IsNullOrWhiteSpace(field))
            return persons;

        var sortedPersons = (field, sortOptions) switch
        {
            (nameof(PersonResponse.PersonName), SortOptions.Asc) => persons.OrderBy(p => p.PersonName).ToList(),
            (nameof(PersonResponse.PersonName), SortOptions.Desc) => persons.OrderByDescending(p => p.PersonName)
                .ToList(),
            (nameof(PersonResponse.Email), SortOptions.Asc) => persons.OrderBy(p => p.Email).ToList(),
            (nameof(PersonResponse.Email), SortOptions.Desc) => persons.OrderByDescending(p => p.Email).ToList(),
            (nameof(PersonResponse.Gender), SortOptions.Asc) => persons.OrderBy(p => p.Gender).ToList(),
            (nameof(PersonResponse.Gender), SortOptions.Desc) => persons.OrderByDescending(p => p.Gender).ToList(),
            (nameof(PersonResponse.Country), SortOptions.Asc) => persons.OrderBy(p => p.Country).ToList(),
            (nameof(PersonResponse.Country), SortOptions.Desc) => persons.OrderByDescending(p => p.Country).ToList(),
            _ => persons
        };

        return sortedPersons;
    }

    public PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if(personUpdateRequest == null)
            throw new ArgumentNullException(nameof(personUpdateRequest));
        
        ValidationHelper.ModelValidation(personUpdateRequest);

        var person = _persons.FirstOrDefault(x => x.PersonId == personUpdateRequest.PersonId);
        if (person ==null)
            throw new ArgumentException("Given person id is invalid");
        
        person.PersonName = personUpdateRequest.PersonName;
        person.Email = personUpdateRequest.Email;
        
        return person.ToPersonResponse();
    }
}