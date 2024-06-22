using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IStreetBookService
{
    StreetBookViewModel GetStreetBook();
}

public class StreetBookService(IHostEnvironment hostEnvironment) : IStreetBookService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        TypeInfoResolver = PersonViewModelContext.Default
    };

    public StreetBookViewModel GetStreetBook()
    {
        var basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
        var peopleFilePath = Path.Combine(basePath, "people.json");

        var json = File.ReadAllText(peopleFilePath);
        var people = JsonSerializer.Deserialize<List<PersonViewModel>>(json, _jsonSerializerOptions) ?? [];

        return new()
        {
            Persons = people.Where(people => !people.IsHidden).ToList(),
        };
    }
}
