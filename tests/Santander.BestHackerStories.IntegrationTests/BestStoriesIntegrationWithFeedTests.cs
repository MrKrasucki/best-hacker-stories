using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Santander.BestHackerStories.API.Contracts;

namespace Santander.BestHackerStories.IntegrationTests;

public class BestStoriesIntegrationWithFeedTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BestStoriesIntegrationWithFeedTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBestHackerStories_ReturnsOkAndTenResults_WhenGivenSomeTimeAfterStartup()
    {
        // Act
        var executionCount = 0;
        ICollection<BestStoryDetails> stories = [];

        while (executionCount < 6)
        {
            var response = await _client.GetAsync("best-stories?storyCount=10");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            stories = await response.Content.ReadFromJsonAsync<ICollection<BestStoryDetails>>() ?? [];
            
            Assert.NotNull(stories);

            if (stories.Any()) break;

            executionCount++;
            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        // Assert
        Assert.NotEmpty(stories);
        Assert.Equal(10, stories.Count); // this will fail if there's less than 10 hackers stories
    }
}
