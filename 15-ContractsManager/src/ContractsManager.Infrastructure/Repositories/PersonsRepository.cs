using System.Linq.Expressions;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class PersonsRepository(ApplicationDbContext db) : IPersonsRepository
{
    public async Task<Person> AddPerson(Person person)
    {
        await db.Persons.AddAsync(person);
        await db.SaveChangesAsync();
        return person;
    }

    public async Task<List<Person>> GetAllPersons()
    {
        return await db.Persons.Include("Country").ToListAsync();
    }

    public async Task<Person> GetPersonById(Guid personId)
    {
        return await db.Persons.FindAsync(personId);
    }

    public Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
    {
        return db.Persons
            .Include("Country")
            .Where(predicate).ToListAsync();
    }

    public async Task<bool> DeletePersonById(Guid personId)
    {
        db.Persons.RemoveRange(db.Persons.Where(x => x.PersonId == personId));
        var rowsDeleted = await db.SaveChangesAsync();
        return rowsDeleted > 0;
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        var existingPerson = await db.Persons.FindAsync(person.PersonId);
        if (existingPerson == null)
            return person;
        
        existingPerson.PersonName = person.PersonName;
        existingPerson.Email = person.Email;
        existingPerson.DateOfBirth = person.DateOfBirth;
        existingPerson.Gender = person.Gender;
        existingPerson.CountryId = person.CountryId;
        existingPerson.Address = person.Address;
        existingPerson.ReceiveNewsletter = person.ReceiveNewsletter;
 
        var countUpdated = await db.SaveChangesAsync();
        return person;
    }
}