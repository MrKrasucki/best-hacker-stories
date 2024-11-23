using Microsoft.AspNetCore.Mvc;
using BestHackerStories.API;
using BestHackerStories.API.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(Constants.HackerNewsApiClient, httpClient => {
    httpClient.BaseAddress = builder.Configuration.GetValue<Uri>("HackerNewsApi:BaseUri");
});

builder.Services.AddSingleton<IHackerNewsApiClient, HackerNewsApiClient>();
builder.Services.AddHostedService<HackerNewsFeeder>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/best-stories", ([FromQuery] int? storyCount) => {
    return HackerNewsStore.GetBestStories().Take(storyCount ?? 10).ToList();
})
.WithName("GetBestHackerStories")
.WithOpenApi();

app.Run();

public partial class Program { }