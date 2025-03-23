using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IStreetBookService
{
    (StreetBookViewModel, string) GetStreetBook();
}

public class StreetBookService(
    IHostEnvironment hostEnvironment,
    ILogger<StreetBookService> logger) : IStreetBookService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public (StreetBookViewModel, string) GetStreetBook()
    {
        var basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
        var peopleFilePath = Path.Combine(basePath, "people.json");
        var json = File.ReadAllText(peopleFilePath);

        List<PersonViewModel> people = [];

        try
        {
            people = JsonSerializer.Deserialize<List<PersonViewModel>>(json, _jsonSerializerOptions) ?? [];
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Failed to deserialize people.json");
            return (new() { Persons = [] }, "Failed to deserialize people.json");
        }

        return (new()
        {
            Persons = [.. people.Where(people => !people.IsHidden)],
        },
        string.Empty);
    }
}
