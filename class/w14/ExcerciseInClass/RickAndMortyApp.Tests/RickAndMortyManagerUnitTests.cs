using System.Threading.Tasks;
using Moq;

namespace RickAndMortyApp.Tests;

[TestClass]
[TestCategory("Unit")]
public sealed class RickAndMortyManagerUnitTests
{
    [TestMethod]
    public async Task GetEpisodesWhereCharacterIsPressent_ReturnWhenCharacterExcists()
    {
        // Arrange
        var mockApiClient = new Mock<IRickAndMortyApiClient>();

        mockApiClient
            .Setup(client => client.GetAsync("https://rickandmortyapi.com/api/character?name=Squanchy"))
            .ReturnsAsync(@"{
                ""results"": [
                {
                    ""id"": 1,
                    ""name"": ""Rick Sanchez"",
                    ""episode"": [
                        ""https://rickandmortyapi.com/api/episode/11"",
                        ""https://rickandmortyapi.com/api/episode/16"",
                        ""https://rickandmortyapi.com/api/episode/21""
                    ]
                }]
            }");

        mockApiClient
            .Setup(client => client.GetAsync("https://rickandmortyapi.com/api/episode/11"))
            .ReturnsAsync(@"{""episode"": ""S01E11""}");

        mockApiClient
            .Setup(client => client.GetAsync("https://rickandmortyapi.com/api/episode/16"))
            .ReturnsAsync(@"{""episode"": ""S02E05""}");

        mockApiClient
            .Setup(client => client.GetAsync("https://rickandmortyapi.com/api/episode/21"))
            .ReturnsAsync(@"{""episode"": ""S02E10"" }");

        var manager = new RickAndMortyManager(mockApiClient.Object);

        // Act
        var episodes = await manager.GetEpisodesWhereCharacterIsPresent("Squanchy");

        // Assert
        Assert.AreEqual(3, episodes.Count);
        Assert.IsTrue(episodes.Contains("S01E11"));
        Assert.IsTrue(episodes.Contains("S02E05"));
        Assert.IsTrue(episodes.Contains("S02E10"));

        mockApiClient.Verify(client => client.GetAsync("https://rickandmortyapi.com/api/character?name=Squanchy"), Times.Once);
    }
}
