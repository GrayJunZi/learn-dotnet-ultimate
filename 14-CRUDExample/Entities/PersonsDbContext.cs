using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Entities;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }
    
    private static List<Country> _countries=new List<Country>
    {
        new Country()
        {
            Id = Guid.Parse("FEC019FC-4008-4C62-9485-96A7D38299CA"),
            Name = "Beijing"
        },
        new Country()
        {
            Id = Guid.Parse("209038D2-B47B-4528-B34B-E1E5BA80AD35"),
            Name = "Shanghai"
        },
        new Country()
        {
            Id = Guid.Parse("D99DEA01-7CFF-49C0-A878-12F799F6B2BC"),
            Name = "Guangzhou"
        },
        new Country()
        {
            Id = Guid.Parse("1BCFEA14-2EC5-4F8F-A870-22F07BB3D893"),
            Name = "Shenzhen",
        },
    };

    private static List<Person> _persons = new List<Person>()
    {
        new Person()
        {
            PersonId = Guid.Parse("32859A69-4BDD-489D-9912-F2DAB6B64270"),
            PersonName = "John Doe",
            Address = "123 Main Street",
            DateOfBirth = DateTime.Now.AddYears(-20),
            Email = "john@doe.com",

        }
    };

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Person>().ToTable("Persons");
        
        modelBuilder.Entity<Country>().HasData(_countries);
        
        modelBuilder.Entity<Person>().HasData(_persons);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // 忽略警告，否则将无法执行迁移
        optionsBuilder.ConfigureWarnings(warning => 
            warning.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    public List<Person> sp_GetAllPersons()
    {
        return Persons.FromSqlRaw("EXECUTE [dbo].[sp_GetAllPersons]").ToList();
    }
}