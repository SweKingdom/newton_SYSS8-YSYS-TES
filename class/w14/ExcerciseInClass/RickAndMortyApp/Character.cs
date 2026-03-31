namespace RickAndMortyApp;
using System;

public class Character
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public List<string> Episode { get; set; } = new();
}
