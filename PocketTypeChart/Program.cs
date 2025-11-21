using Application.Abstractions.Repositories;
using Application.PokeTypes.GetAllTypes;
using Application.PokeTypes.PreloadTypes;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PocketTypeChart.Extensions.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PokeDbContext>(opt => opt.UseSqlServer(cs));
builder.Services.AddScoped<IPokeTypeRepository, PokeTypeRepository>();
builder.Services.AddScoped<IPokeTypeRelationRepository, PokeTypeRelationRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<PreloadTypesCommand>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.MapGet("/api/poketypes", async (IMediator mediator) =>
{
    var getAllTypes = new GetAllTypesQuery();
    var result = await mediator.Send(getAllTypes);
    return result.ToHttpResult();
}).WithName("GetAllPokeTypes");

app.MapPost("/api/poketypes/preloadTypes", async (IMediator mediator) =>
{
    var preloadTypes = new PreloadTypesCommand();
    var result = await mediator.Send(preloadTypes);
    return result.ToHttpResult();
}).WithName("PreloadTypes");

app.UseHttpsRedirection();

app.Run();
