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


    public IEnumerable<PersonResponse> GetFilteredPersons(string field, string? search)
    {
        var persons = GetAllPersons();

        if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(search))
            return persons;

        switch (field)
        {
            case nameof(Person.PersonName):
                return persons.Where(p => p.PersonName.Contains(search));
            case nameof(Person.Email):
                return persons.Where(p => p.Email.Contains(search));
            case nameof(Person.Gender):
                return persons.Where(p => p.Gender == search);
            case nameof(Person.Address):
                return persons.Where(p => p.Address.Contains(search));
            default:
                return persons;
        }
    }

    public IEnumerable<PersonResponse> GetSortedPersons(string? field, SortOptions sortOptions)
    {
        throw new NotImplementedException();
    }
}