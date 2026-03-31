namespace RickAndMortyApp;

using System.Text.Json;

public class RickAndMortyManager
{
    private readonly IRickAndMortyApiClient _apiClient;

    public RickAndMortyManager()
    {
        _apiClient = new RickAndMortyApiClient();
    }

    public RickAndMortyManager(IRickAndMortyApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<string>> GetEpisodesWhereCharacterIsPresent(string characterName)
    {
        var json = await _apiClient.GetAsync($"https://rickandmortyapi.com/api/character?name={characterName}");

          var characterResponse = JsonSerializer.Deserialize<CharacterResponse>(json,
              new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var character = characterResponse?.Results.FirstOrDefault();
        if (character == null)
            return new List<string>();

        var episodeCodes = new List<string>();
        foreach (var episodeUrl in character.Episode)
        {
            var episodeJson = await _apiClient.GetAsync(episodeUrl);
            var episode = JsonSerializer.Deserialize<Episode>(episodeJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (episode != null) episodeCodes.Add(episode.EpisodeCode);
        }

        return episodeCodes;
    }
}