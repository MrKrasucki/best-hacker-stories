using System;
using Santander.BestHackerStories.API.Contracts;

namespace Santander.BestHackerStories.API;

public interface IHackerNewsApiClient 
{
    Task<ICollection<int>> GetBestStories();
    Task<ItemDetailsResponse?> GetStoryDetails(int id);
}

public sealed class HackerNewsApiClient(IHttpClientFactory httpClientFactory) 
    : IHackerNewsApiClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Constants.HackerNewsApiClient);

    public async Task<ICollection<int>> GetBestStories()
    {
        var response = await _httpClient.GetFromJsonAsync<ICollection<int>>("/v0/beststories.json");
        return response ?? Array.Empty<int>();
    }
    
    public async Task<ItemDetailsResponse?> GetStoryDetails(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<ItemDetailsResponse>($"/v0/item/{id}.json");
        return response;
    }
}
