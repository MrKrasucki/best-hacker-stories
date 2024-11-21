using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Santander.BestHackerStories.API.Contracts;

namespace Santander.BestHackerStories.IntegrationTests;

public class BestStoriesIntegrationAfterStartupTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BestStoriesIntegrationAfterStartupTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBestHackerStories_ReturnsOkAndEmptyArray_WhenCalledRightAfterStartupWithNoQueryParam()
    {
        // Act
        var response = await _client.GetAsync("best-stories");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var stories = await response.Content.ReadFromJsonAsync<ICollection<BestStoryDetails>>();
        Assert.NotNull(stories);
        Assert.Empty(stories);
    }
    
    [Fact]
    public async Task GetBestHackerStories_ReturnsOkAndEmptyArray_WhenCalledRightAfterStartupWithQueryParam()
    {
        // Act
        var response = await _client.GetAsync("best-stories?storyCount=15");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var stories = await response.Content.ReadFromJsonAsync<ICollection<BestStoryDetails>>();
        Assert.NotNull(stories);
        Assert.Empty(stories);
    }
}
