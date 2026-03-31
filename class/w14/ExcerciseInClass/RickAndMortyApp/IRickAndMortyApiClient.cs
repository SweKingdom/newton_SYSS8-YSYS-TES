namespace RickAndMortyApp;
using System;

public interface IRickAndMortyApiClient
{
    Task<string> GetAsync(string url);
}
