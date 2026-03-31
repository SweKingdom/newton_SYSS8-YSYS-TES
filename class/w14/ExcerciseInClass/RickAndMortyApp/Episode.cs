namespace RickAndMortyApp;

using System;
using System.Text.Json.Serialization;

public class Episode
{
    [JsonPropertyName("episode")]
    public string EpisodeCode { get; set; } = string.Empty;
}
