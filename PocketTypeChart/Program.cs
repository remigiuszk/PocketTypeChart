var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PokeDbContext>(opt => opt.useSqlServer(cs));
builder.Services.AddScoped<IPokeTypeRepository, PokeTypeRepository>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
