# best-hacker-stories
RESTful API that retrieves the details of the best n stories from the Hacker News API, as determined by their score, where n is specified by the caller to the API.

## Features
- RESTful API endpoint that returns requested best n stories from the Hacker News API 
- Swagger for API documentation
- Dependency injection setup
- BackgroundService that feeds the hacker news in 5 minutes intervals

## Prerequisites
Make sure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- [Git](https://git-scm.com/)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

## Getting Started

### Clone the Repository
```bash
git clone https://github.com/MrKrasucki/best-hacker-stories.git
cd best-hacker-stories\src\Santander.BestHackerStories.API
```

### Run the solution
```bash
dotnet build
dotnet run
```

Note: please wait until initial feed completes.

### Run the integration tests
```bash
cd best-hacker-stories\tests\Santander.BestHackerStories.IntegrationTests
dotnet test
```

## Assumptions

I assumed that performance is more important than getting real-time data. That's why I chose to get data once in 5 minutes intervals instead of fetching them on every request.
Fetching data takes 30 seconds on my machine in Release mode. We need to take under consideration that HackerNewsStore will be empty until initial feed completes.

## Future improvements

Given the time I could work on multiple aspects of the application. I could:
- refactor HackerNewsStore using System.Reactive library as a Subject
- add more Unit, Integration and Contract Tests
- try and use Firebase libraries. Hacker News API documentation says that I could listen to changes instead of polling data
- add a rate limiter so the API is not DDoS-ed
- add a health check for seeing how many times feeder executed
- run the initial feed on start up so API is available right away
