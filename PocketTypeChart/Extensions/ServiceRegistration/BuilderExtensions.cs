using Application.Abstractions.Repositories;
using Application.PokeTypes.PreloadTypes;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Application.Abstractions.Services;
using Application.External.PokeApi;
using Application.Abstractions.Queries;
using DataAccess.Queries;
using System.Threading.RateLimiting;
using System.Net;

namespace PocketTypeChart.Extensions.ServiceRegistration
{
    public static class BuilderExtensions
    {
        public static void RegisterBuilderServices(this WebApplicationBuilder builder)
        {
            RegisterLogging(builder);

            RegisterDatabaseServices(builder);

            RegisterApplicationServices(builder);

            RegisterRateLimiter(builder);

            builder.Services.AddMemoryCache();

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

            builder.Services.AddDbContext<PokeDbContext>(opt => opt.UseSqlServer(connectionString, sql =>
            {
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                sql.CommandTimeout(60);
            }));

            RegisterRepositoriesAndQueries(builder);
        }

        private static void RegisterRepositoriesAndQueries(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPokeTypeRepository, PokeTypeRepository>();
            builder.Services.AddScoped<IDamageRelationRepository, DamageRelationRepository>();

            builder.Services.AddScoped<IDamageRelationQueries, DamageRelationQueries>();
        }

        private static void RegisterApplicationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPokeApiHttpService, PokeApiHttpService>();
        }

        private static void RegisterRateLimiter(WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("global", context =>
                {
                    var ip = GetClientIp(context) ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 120,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            AutoReplenishment = true
                        });
                });
            });
        }
        private static string? GetClientIp(HttpContext context)
        {
            // Jeśli jesteś za proxy (Azure/App Service), to często dostaniesz X-Forwarded-For.
            // Bierzemy pierwsze IP z listy.
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwarded))
            {
                var first = forwarded.ToString().Split(',')[0].Trim();
                if (IPAddress.TryParse(first, out var ip))
                    return ip.ToString();
            }

            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
