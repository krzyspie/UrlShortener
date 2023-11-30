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

var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
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

app.MapGet("/{link}", async (IMediator mediator, string link) =>
{
    GetSourceUrl query = new()
    {
        ShortUrl = link
    };

    var result = await mediator.Send(query);

    return TypedResults.Ok(result);
});

app.Run();