using Application.Abstractions.Repositories;
using Application.PokeTypes.PreloadTypes;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace PocketTypeChart.Extensions.ServiceRegistration
{
    public static class BuilderExtensions
    {
        public static void RegisterBuilderServices(this WebApplicationBuilder builder)
        {
            RegisterLogging(builder);

            RegisterDatabaseServices(builder);

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<PreloadTypesCommand>());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static void RegisterLogging(WebApplicationBuilder builder)
        {
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });
        }

        private static void RegisterDatabaseServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<PokeDbContext>(opt => opt.UseSqlServer(connectionString));
            RegisterRepositories(builder);
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPokeTypeRepository, PokeTypeRepository>();
            builder.Services.AddScoped<IPokeTypeRelationRepository, PokeTypeRelationRepository>();
        }
    }
}
