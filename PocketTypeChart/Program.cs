using PocketTypeChart.Endpoints;
using PocketTypeChart.Extensions.ServiceRegistration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterBuilderServices();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.RegisterDamageRelationEndpoints();
app.RegisterPokeTypeEndpoints();

app.UseHttpsRedirection();
app.UseRateLimiter();

app.Run();
