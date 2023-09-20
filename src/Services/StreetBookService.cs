using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IStreetBookService
{
    StreetBookViewModel GetStreetBook();
}

public class StreetBookService : IStreetBookService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly string _basePath;

    public StreetBookService(IHostEnvironment hostEnvironment)
    {
        _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        _basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
    }

    public StreetBookViewModel GetStreetBook()
    {
        var peopleFilePath = Path.Combine(_basePath, "people.json");
        var json = File.ReadAllText(peopleFilePath);
        var people = JsonSerializer.Deserialize<List<PersonViewModel>>(json, _jsonSerializerOptions) ?? new();

        return new()
        {
            Persons = people
        };
    }
}
