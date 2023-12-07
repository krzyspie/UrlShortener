using Application.Commands;
using Application.Interfaces;
using Application.Queries;
using Application.Services;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
builder.Services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();
builder.Services.AddSingleton<IUrlValidator, UrlValidator>();

var redisUrl = builder.Configuration.GetValue<string>("RedisUrl");
var multiplexer = ConnectionMultiplexer.Connect(redisUrl);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddSingleton<IUrlRepository, UrlRepository>();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateShortUrl).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/shorturl", async Task<Results<BadRequest<string>, Ok<string>>>(IMediator mediator, IUrlValidator urlValidator, string url) =>
{
    var isValidUrl = urlValidator.IsValid(url);

    if (!isValidUrl)
    {
        return TypedResults.BadRequest("Url not valid.");
    }

    CreateShortUrl command = new()
    {
        OriginUrl = url
    };

    var result = await mediator.Send(command);

    return TypedResults.Ok(result);
})
.WithName("CreateShortUrl");

app.MapGet("/{link}", async Task<Results<BadRequest<string>, Ok<string>, NotFound<string>>> (IMediator mediator, string link) =>
{
    if (string.IsNullOrWhiteSpace(link))
    {
        return TypedResults.BadRequest("Link not provided");
    }

    GetSourceUrl query = new()
    {
        ShortUrl = link
    };

    var result = await mediator.Send(query);

    return string.IsNullOrEmpty(result) ? TypedResults.NotFound($"Url for '{link}' not found.") : TypedResults.Ok(result);
});

app.Run();