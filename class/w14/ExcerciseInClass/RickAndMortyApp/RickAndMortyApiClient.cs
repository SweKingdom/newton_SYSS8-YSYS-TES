namespace RickAndMortyApp;
using System;

public class RickAndMortyApiClient : IRickAndMortyApiClient
{
    private readonly HttpClient _httpClient = new();

    public async Task<string> GetAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
