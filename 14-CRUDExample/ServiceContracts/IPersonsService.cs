using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonsService
{
    PersonResponse? AddPerson(PersonAddRequest? personAddRequest);
    List<PersonResponse> GetAllPersons();
    PersonResponse? GetPersonByPersonId(Guid? personId);

    List<PersonResponse> GetFilteredPersons(string field, string? search);
    List<PersonResponse> GetSortedPersons(List<PersonResponse> persons, string? field, SortOptions sortOptions);
    PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest);
}