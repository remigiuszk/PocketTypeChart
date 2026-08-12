using Application.PokeTypes.GetAllTypes;
using Application.PokeTypes.PreloadTypes;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PocketTypeChart.Extensions.Application;

namespace PocketTypeChart.Endpoints
{
    public static class PokeTypeEndpoints
    {
        public static void RegisterPokeTypeEndpoints(this WebApplication app)
        {
            var posts = app.MapGroup("/api/poketypes").RequireRateLimiting("global");

            posts.MapGet("/", GetAllPokeTypes);
            posts.MapMethods("/health", ["GET", "HEAD"], WarmUpDb);
            //posts.MapPost("/preloadTypes", PreloadPokeTypes);
        }

        private static async Task<IResult> GetAllPokeTypes(IMediator mediator, IMemoryCache cache)
        {
            var result = await cache.GetOrCreateAsync("poketypes_all_v1", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                entry.Priority = CacheItemPriority.NeverRemove;


                var res = await mediator.Send(new GetAllTypesQuery());
                if (res.IsFailure) return null;
                return res.Value;
            });

            if (result is null)
                return Results.Problem("Failed to load poke types.");

            return Results.Ok(result);
        }

        private static async Task<IResult> PreloadPokeTypes(IMediator mediator)
        {
            var preloadTypes = new PreloadTypesCommand();
            var result = await mediator.Send(preloadTypes);
            return result.ToHttpResult();
        }

        private static async Task<IResult> WarmUpDb(IMediator mediator)
        {
            await mediator.Send(new HealthQuery());
            return Results.Ok();
        }
    }
}
