using System.Collections.Generic;
using System.Text.Json;

namespace StreetBook.Models.ViewModels;

public class StreetBookViewModel
{
    public List<PersonViewModel> Persons { get; set; } = [];

    public Dictionary<string, int> HighScores { get; set; } = [];

    public string PersonsJson => JsonSerializer.Serialize(Persons);
}
