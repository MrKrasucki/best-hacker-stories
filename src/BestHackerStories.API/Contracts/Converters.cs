using System;

namespace BestHackerStories.API.Contracts;

public static class Converters
{
    public static BestStoryDetails ToBestStoryDetails(this ItemDetailsResponse itemDetails)
    {
        return new BestStoryDetails(
            itemDetails.Title,
            itemDetails.Url,
            itemDetails.By,
            new DateTime(itemDetails.Time),
            itemDetails.Score,
            itemDetails.Descendants);    
    }
}
