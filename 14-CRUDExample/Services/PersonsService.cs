using System.Drawing;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonsService : IPersonsService
{
    private readonly ApplicationDbContext _db;
    private readonly ICountriesService _countriesService;

    public PersonsService(ApplicationDbContext db, ICountriesService countriesService)
    {
        _db = db;
        _countriesService = countriesService;
    }

    public async Task<PersonResponse?> AddPerson(PersonAddRequest? personAddRequest)
    {
        if (personAddRequest == null)
            throw new ArgumentNullException(nameof(personAddRequest));

        ValidationHelper.ModelValidation(personAddRequest);

        var person = personAddRequest.ToPerson();

        person.PersonId = Guid.NewGuid();

        // _db.Persons.Add(person);
        // _db.SaveChanges();

        _db.sp_InsertPerson(person);

        return person.ToPersonResponse();
    }

    public async Task<List<PersonResponse>> GetAllPersons()
    {
        // var person = _db.Persons.Include("Country").ToList();

        //return _db.Persons.Select(convertPersonResponse).ToList();
        return _db.sp_GetAllPersons().Select(x => x.ToPersonResponse()).ToList();
    }

    public async Task<PersonResponse?> GetPersonByPersonId(Guid? personId)
    {
        if (personId == null)
            return null;
        return (await _db.Persons?.FirstOrDefaultAsync(p => p.PersonId == personId))?.ToPersonResponse();
    }


    public async Task<List<PersonResponse>> GetFilteredPersons(string? field, string? search)
    {
        var persons = await GetAllPersons();

        if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(search))
            return persons;

        switch (field)
        {
            case nameof(PersonResponse.PersonName):
                return persons.Where(p => p.PersonName.Contains(search)).ToList();
            case nameof(PersonResponse.Email):
                return persons.Where(p => p.Email.Contains(search)).ToList();
            case nameof(PersonResponse.Gender):
                return persons.Where(p => p.Gender == search).ToList();
            case nameof(PersonResponse.Country):
                return persons.Where(p => p.Country == search).ToList();
            case nameof(PersonResponse.Address):
                return persons.Where(p => p.Address.Contains(search)).ToList();
            default:
                return persons;
        }
    }

    public async Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> persons, string? field,
        SortOptions? sortOptions)
    {
        if (string.IsNullOrWhiteSpace(field))
            return persons;

        var sortedPersons = (field, sortOptions) switch
        {
            (nameof(PersonResponse.PersonName), SortOptions.Asc) => persons.OrderBy(p => p.PersonName).ToList(),
            (nameof(PersonResponse.PersonName), SortOptions.Desc) => persons.OrderByDescending(p => p.PersonName)
                .ToList(),
            (nameof(PersonResponse.Email), SortOptions.Asc) => persons.OrderBy(p => p.Email).ToList(),
            (nameof(PersonResponse.Email), SortOptions.Desc) => persons.OrderByDescending(p => p.Email).ToList(),
            (nameof(PersonResponse.Gender), SortOptions.Asc) => persons.OrderBy(p => p.Gender).ToList(),
            (nameof(PersonResponse.Gender), SortOptions.Desc) => persons.OrderByDescending(p => p.Gender).ToList(),
            (nameof(PersonResponse.Country), SortOptions.Asc) => persons.OrderBy(p => p.Country).ToList(),
            (nameof(PersonResponse.Country), SortOptions.Desc) => persons.OrderByDescending(p => p.Country).ToList(),
            _ => persons
        };

        return sortedPersons;
    }

    public async Task<PersonResponse?> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest == null)
            throw new ArgumentNullException(nameof(personUpdateRequest));

        ValidationHelper.ModelValidation(personUpdateRequest);

        var person = await _db.Persons.FirstOrDefaultAsync(x => x.PersonId == personUpdateRequest.PersonId);
        if (person == null)
            throw new ArgumentException("Given person id is invalid");

        person.PersonName = personUpdateRequest.PersonName;
        person.Email = personUpdateRequest.Email;
        person.Gender = personUpdateRequest.Gender.ToString();
        person.DateOfBirth = personUpdateRequest.DateOfBirth;
        person.Address = personUpdateRequest.Address;

        return person.ToPersonResponse();
    }

    public async Task<bool> DeletePerson(Guid? personId)
    {
        if (personId == null)
            throw new ArgumentNullException(nameof(personId));

        var person = await _db.Persons.FirstOrDefaultAsync(x => x.PersonId == personId);
        if (person == null)
            return false;

        _db.Persons.Remove(person);
        return true;
    }

    public async Task<MemoryStream> GetPersonsCSV()
    {
        var ms = new MemoryStream();
        var sw = new StreamWriter(ms);

        CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

        var csvWriter = new CsvWriter(sw, csvConfiguration);

        // csvWriter.WriteHeader<PersonResponse>();

        csvWriter.WriteField(nameof(PersonResponse.PersonName));
        csvWriter.WriteField(nameof(PersonResponse.Email));
        csvWriter.WriteField(nameof(PersonResponse.DateOfBirth));
        csvWriter.WriteField(nameof(PersonResponse.Age));
        csvWriter.WriteField(nameof(PersonResponse.Gender));
        csvWriter.WriteField(nameof(PersonResponse.Country));
        csvWriter.WriteField(nameof(PersonResponse.Address));
        csvWriter.WriteField(nameof(PersonResponse.ReceiveNewsletter));

        await csvWriter.NextRecordAsync();

        var persons = await _db
            .Persons
            .Include("Country")
            .Select(x => x.ToPersonResponse())
            .ToListAsync();

        // await csvWriter.WriteRecordsAsync(persons);

        foreach (var person in persons)
        {
            csvWriter.WriteField(person.PersonName);
            csvWriter.WriteField(person.Email);
            csvWriter.WriteField(person.DateOfBirth.Value.ToString("yyyy-MM-dd"));
            csvWriter.WriteField(person.Age);
            csvWriter.WriteField(person.Gender);
            csvWriter.WriteField(person.Country);
            csvWriter.WriteField(person.Address);
            csvWriter.WriteField(person.ReceiveNewsletter);
            await csvWriter.NextRecordAsync();
            await csvWriter.FlushAsync();
        }

        ms.Position = 0;
        return ms;
    }

    public async Task<MemoryStream> GetPersonsExcel()
    {
        ExcelPackage.License.SetNonCommercialPersonal("crud");
        var ms = new MemoryStream();
        using var excelPackage = new ExcelPackage(ms);

        var worksheet = excelPackage.Workbook.Worksheets.Add("Persons");

        worksheet.Cells["A1"].Value = "Person Name";
        worksheet.Cells["B1"].Value = "Email";
        worksheet.Cells["C1"].Value = "DateOfBirth";
        worksheet.Cells["D1"].Value = "Age";
        worksheet.Cells["E1"].Value = "Gender";
        worksheet.Cells["F1"].Value = "Country";
        worksheet.Cells["G1"].Value = "Address";
        worksheet.Cells["H1"].Value = "ReceiveNewsletter";

        using var headerCells = worksheet.Cells["A1:H1"];
        headerCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
        headerCells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
        headerCells.Style.Font.Bold = true;

        var row = 2;
        var persons = await _db.Persons
            .Include("Country")
            .Select(x => x.ToPersonResponse())
            .ToListAsync();

        foreach (var person in persons)
        {
            worksheet.Cells[row, 1].Value = person.PersonName;
            worksheet.Cells[row, 2].Value = person.Email;
            if (person.DateOfBirth.HasValue)
                worksheet.Cells[row, 3].Value = person.DateOfBirth;
            worksheet.Cells[row, 4].Value = person.Age;
            worksheet.Cells[row, 5].Value = person.Gender;
            worksheet.Cells[row, 6].Value = person.Country;
            worksheet.Cells[row, 7].Value = person.Address;
            worksheet.Cells[row, 8].Value = person.ReceiveNewsletter;

            row++;
        }

        worksheet.Cells[$"A1:H{row}"].AutoFitColumns();

        await excelPackage.SaveAsync();

        ms.Position = 0;
        return ms;
    }
}