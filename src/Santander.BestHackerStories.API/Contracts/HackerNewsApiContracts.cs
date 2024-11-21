using System;

namespace Santander.BestHackerStories.API.Contracts;

public sealed record BestStoriesResponse(ICollection<int> BestStoriesIds);

public sealed record ItemDetailsResponse(string Title, Uri Url, string By, long Time, int Score, int Descendants);
