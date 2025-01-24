using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonsService
{
    PersonResponse? AddPerson(PersonAddRequest? personAddRequest);
    List<PersonResponse> GetAllPersons();
    PersonResponse? GetPersonByPersonId(Guid? personId);

    IEnumerable<PersonResponse> GetFilteredPersons(string field, string? search);
    IEnumerable<PersonResponse> GetSortedPersons(string? field, SortOptions sortOptions);
}