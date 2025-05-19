using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonsService
{
    Task<PersonResponse?> AddPerson(PersonAddRequest? personAddRequest);
    Task<List<PersonResponse>> GetAllPersons();
    Task<PersonResponse?> GetPersonByPersonId(Guid? personId);

    Task<List<PersonResponse>> GetFilteredPersons(string field, string? search);
    Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> persons, string? field, SortOptions? sortOptions);
    Task<PersonResponse?> UpdatePerson(PersonUpdateRequest? personUpdateRequest);
    Task<bool> DeletePerson(Guid? personId);
    Task<MemoryStream> GetPersonsCSV();
}