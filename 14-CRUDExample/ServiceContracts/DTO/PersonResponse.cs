using Entities;

namespace ServiceContracts.DTO;

public class PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string Address { get; set; }
    public bool ReceiveNewsletter { get; set; }
    
    public string Country { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not PersonResponse personResponse)
            return false;

        return personResponse.PersonId == PersonId
               && personResponse.PersonName == PersonName
               && personResponse.Email == Email
               && personResponse.DateOfBirth == DateOfBirth
               && personResponse.Gender == Gender
               && personResponse.CountryId == CountryId
               && personResponse.Address == Address
               && personResponse.ReceiveNewsletter == ReceiveNewsletter;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public static class PersonResponseExtensions
{
    public static PersonResponse ToPersonResponse(this Person person) => new PersonResponse
    {
        PersonId = person.PersonId,
        PersonName = person.PersonName,
        Email = person.Email,
        DateOfBirth = person.DateOfBirth,
        Gender = person.Gender,
        CountryId = person.CountryId,
        Address = person.Address,
        ReceiveNewsletter = person.ReceiveNewsletter
    };
}