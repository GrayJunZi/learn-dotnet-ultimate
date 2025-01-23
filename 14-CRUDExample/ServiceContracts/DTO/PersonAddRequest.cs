using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonAddRequest
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string Address { get; set; } 
    public bool ReceiveNewsletter { get; set; }
    
    public Person ToPerson()
    {
        return new Person
        {
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString(),
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsletter = ReceiveNewsletter
        };
    }
}