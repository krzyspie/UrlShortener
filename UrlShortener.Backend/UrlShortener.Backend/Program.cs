using Application.Commands;
using Application.Interfaces;
using Application.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
builder.Services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();

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

app.MapPost("/shorturl", (IMediator mediator, string url) =>
{
    CreateShortUrl command = new()
    {
        Url = url
    };

    var result = mediator.Send(command);

    return result;
})
.WithName("CreateShortUrl");

app.Run();