using System;
using BestHackerStories.API.Contracts;

namespace BestHackerStories.API;

public static class HackerNewsStore
{
    private static ICollection<BestStoryDetails> _bestStories = [];

    public static ICollection<BestStoryDetails> GetBestStories() => _bestStories;
    public static void SetBestStories(ICollection<BestStoryDetails> bestStories) => _bestStories = bestStories;
}
