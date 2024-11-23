using System;

namespace BestHackerStories.API.Contracts;

public sealed record BestStoryDetails(string Title, Uri Uri, string PostedBy, DateTime Time, int Score, int CommentCount);