using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
builder.Services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/shorturl", (string longUrl) =>
{
    return "abcd";
})
.WithName("CreateShortUrl");

app.Run();