﻿using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public record PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public double? Age { get; set; }
    public Guid? CountryId { get; set; }
    public string Address { get; set; }
    public bool ReceiveNewsletter { get; set; }

    public string Country { get; set; }
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
        Age = (person.DateOfBirth != null)
            ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25)
            : null,
        CountryId = person.CountryId,
        Country = person.Country?.Name,
        Address = person.Address,
        ReceiveNewsletter = person.ReceiveNewsletter
    };

    public static PersonUpdateRequest ToPersonUpdateRequest(this PersonResponse person) => new PersonUpdateRequest
    {
        PersonId = person.PersonId,
        PersonName = person.PersonName,
        Email = person.Email,
        DateOfBirth = person.DateOfBirth,
        Gender = Enum.Parse<GenderOptions>(person.Gender),
        CountryId = person.CountryId,
        Address = person.Address,
        ReceiveNewsletter = person.ReceiveNewsletter
    };
}