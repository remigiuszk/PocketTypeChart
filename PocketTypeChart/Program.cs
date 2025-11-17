using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PokeDbContext>(opt => opt.UseSqlServer(cs));
builder.Services.AddScoped<IPokeTypeRepository, PokeTypeRepository>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
