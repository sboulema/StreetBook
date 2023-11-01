using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace StreetBook.Services;

public interface IPlayService
{
    Task<Dictionary<string, int>> GetHighscores();

    Task AddHighscore(string name, int score);
}

public class PlayService : IPlayService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly string _basePath;

    public PlayService(IHostEnvironment hostEnvironment)
    {
        _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        _basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
    }

    public async Task<Dictionary<string, int>> GetHighscores()
    {
        var path = Path.Combine(_basePath, "highscores.json");

        if (!File.Exists(path))
        {
            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(new Dictionary<string, int>()));
            return new();
        }

        var json = await File.ReadAllTextAsync(path);
        var scores = JsonSerializer.Deserialize<Dictionary<string, int>>(json, _jsonSerializerOptions) ?? new();

        return scores
            .OrderByDescending(score => score.Value)
            .ToDictionary(score => score.Key, score => score.Value);
    }

    public async Task AddHighscore(string name, int score)
    {
        var scores = await GetHighscores();

        scores.Add(name, score);

        var path = Path.Combine(_basePath, "highscores.json");
        var json = JsonSerializer.Serialize(scores);
        await File.WriteAllTextAsync(path, json);
    }
}
