using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IStreetBookService
{
    StreetBookViewModel GetStreetBook(IHostEnvironment hostEnvironment);
}

public class StreetBookService : IStreetBookService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public StreetBookService()
    {
        _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public StreetBookViewModel GetStreetBook(IHostEnvironment hostEnvironment)
    {
        var peopleFilePath = GetPeopleFilePath(hostEnvironment);
        var json = File.ReadAllText(peopleFilePath);
        var people = JsonSerializer.Deserialize<List<PersonViewModel>>(json, _jsonSerializerOptions) ?? new();

        return new()
        {
            Persons = people
        };
    }

    private static string GetPeopleFilePath(IHostEnvironment hostEnvironment)
    {
        var basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
        var picturePath = Path.Combine(basePath, "people.json");
        return picturePath;
    }
}
