using System.Threading.Tasks;

namespace RickAndMortyApp.Tests;

[TestClass]
[TestCategory("Integration")]
public sealed class RickAndMortyManagerIntegratonTests
{
    [TestMethod]
    public async Task GetEpisodesWhereCharacterIsThere_ReturnsEpisodes_Squanchy()
    {
        // Arrange
        var manager = new RickAndMortyManager();

        // Act
        var episodes = await manager.GetEpisodesWhereCharacterIsPresent("Squanchy");

        // Assert
        Assert.IsTrue(episodes.Count > 0);
        Assert.IsTrue(episodes.Contains("S01E11"));
    }
}
