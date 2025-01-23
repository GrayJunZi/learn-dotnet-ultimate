using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class PersonsService : IPersonsService
{
    private readonly List<Person> _persons = new();
    private readonly ICountriesService _countriesService = new CountriesService();

    public PersonResponse? AddPerson(PersonAddRequest? personAddRequest)
    {
        if (personAddRequest == null)
            throw new ArgumentNullException(nameof(personAddRequest));

        if (string.IsNullOrWhiteSpace(personAddRequest.PersonName))
            throw new ArgumentException($"{nameof(personAddRequest.PersonName)} cannot be empty");

        var person = personAddRequest.ToPerson();

        person.PersonId = Guid.NewGuid();
        _persons.Add(person);

        return convertPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        throw new NotImplementedException();
    }


    private PersonResponse? convertPersonResponse(Person person)
    {
        var personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
        return personResponse;          
    }
}