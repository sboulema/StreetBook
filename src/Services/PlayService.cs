using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace StreetBook.Services;

public interface IPlayService
{
    Task<Dictionary<string, int>> GetHighScores();

    Task AddHighScore(string name, int score);
}

public class PlayService(IHostEnvironment hostEnvironment) : IPlayService
{
    private readonly string _basePath = hostEnvironment.IsProduction()
        ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        TypeInfoResolver = HighScoreContext.Default
    };

    public async Task<Dictionary<string, int>> GetHighScores()
    {
        var path = Path.Combine(_basePath, "highscores.json");

        if (!File.Exists(path))
        {
            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(new Dictionary<string, int>()));
            return [];
        }

        var json = await File.ReadAllTextAsync(path);
        var scores = JsonSerializer.Deserialize<Dictionary<string, int>>(json, _jsonSerializerOptions) ?? [];

        return scores
            .OrderByDescending(score => score.Value)
            .ToDictionary(score => score.Key, score => score.Value);
    }

    public async Task AddHighScore(string name, int score)
    {
        var scores = await GetHighScores();

        scores.Add(name, score);

        var path = Path.Combine(_basePath, "highscores.json");
        var json = JsonSerializer.Serialize(scores);
        await File.WriteAllTextAsync(path, json);
    }
}

[JsonSerializable(typeof(Dictionary<string, int>))]
public partial class HighScoreContext : JsonSerializerContext { }
