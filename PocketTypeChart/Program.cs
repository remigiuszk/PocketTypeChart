using Application.Abstractions.Repositories;
using Application.PokeTypes.PreloadTypes;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<PreloadTypesCommand>());

// Add services to the container.

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
