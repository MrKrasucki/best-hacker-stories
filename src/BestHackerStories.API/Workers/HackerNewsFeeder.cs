using System.Diagnostics;
using BestHackerStories.API.Contracts;

namespace BestHackerStories.API.Workers;

public class HackerNewsFeeder : BackgroundService
{
    private readonly ILogger<HackerNewsFeeder> _logger;
    private readonly IHackerNewsApiClient _client;

    private int _executionCount = 0;

    public HackerNewsFeeder(ILogger<HackerNewsFeeder> logger, IHackerNewsApiClient client)
    {
        _logger = logger;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _executionCount++;

            _logger.LogInformation("Starting to feed best hacker news. Execution count: {Count}", _executionCount);

            try 
            {
                var bestStoriesWithDetails = new List<BestStoryDetails>();
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                var bestStoriesIds = await _client.GetBestStories();

                foreach (var id in bestStoriesIds)
                {
                    var details = await _client.GetStoryDetails(id);
                    if (details is not null){
                        bestStoriesWithDetails.Add(details.ToBestStoryDetails());
                    }
                }

                HackerNewsStore.SetBestStories([.. bestStoriesWithDetails.OrderByDescending(s => s.Score)]);

                stopWatch.Stop();

                _logger.LogInformation("Successfully got the feed from the Hacker News API after {seconds} seconds.", 
                    stopWatch.Elapsed.Seconds);

                await Task.Delay(TimeSpan.FromMinutes(5));
            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Error occured while feeding hacker news.");
            }
        }
    }
}
