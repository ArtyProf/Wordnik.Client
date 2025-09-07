using Microsoft.Extensions.Options;
using Wordnik.Client;
using Wordnik.Demo.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WordnikSettings>(builder.Configuration.GetSection("Wordnik"));

builder.Services.AddHttpClient<IWordnikClient, WordnikClient>((httpClient, serviceProvider) =>
{
    var wordnikApiKey = serviceProvider.GetRequiredService<IOptions<WordnikSettings>>().Value.ApiKey;

    if (string.IsNullOrWhiteSpace(wordnikApiKey))
    {
        throw new InvalidOperationException("Wordnik API_KEY is not set or empty.");
    }

    return new WordnikClient(httpClient, wordnikApiKey);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
