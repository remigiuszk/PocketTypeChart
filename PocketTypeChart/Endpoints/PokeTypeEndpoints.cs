using Application.Abstractions.Queries.Dto;
using Application.PokeTypes.BackfillSpriteImages;
using Application.PokeTypes.GetAllTypes;
using Application.PokeTypes.GetSprite;
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
            posts.MapGet("/{id:int}/sprite", GetSprite);
            posts.MapMethods("/health", ["GET", "HEAD"], WarmUpDb);
            //posts.MapPost("/preloadTypes", PreloadPokeTypes);
            //posts.MapPost("/backfillSpriteImages", BackfillSpriteImages);
        }

        private static async Task<IResult> GetAllPokeTypes(IMediator mediator, IMemoryCache cache)
        {
            var result = await cache.GetOrCreateAsync("poketypes_all_v2", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                entry.Priority = CacheItemPriority.NeverRemove;


                var res = await mediator.Send(new GetAllTypesQuery());
                if (res.IsFailure) return null;
                return res.Value.Select(PokeTypeDto.CreateFromDomain).ToList();
            });

            if (result is null)
                return Results.Problem("Failed to load poke types.");

            return Results.Ok(result);
        }

        private static async Task<IResult> GetSprite(int id, IMediator mediator, IMemoryCache cache, HttpContext httpContext)
        {
            var sprite = await cache.GetOrCreateAsync($"poketype_sprite_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30);
                entry.Priority = CacheItemPriority.NeverRemove;

                var res = await mediator.Send(new GetSpriteQuery(id));
                if (res.IsFailure) return null;
                return res.Value;
            });

            if (sprite is null || sprite.Length == 0)
                return Results.NotFound();

            httpContext.Response.Headers.CacheControl = "public, max-age=31536000, immutable";

            return Results.File(sprite, "image/png");
        }

        private static async Task<IResult> PreloadPokeTypes(IMediator mediator)
        {
            var preloadTypes = new PreloadTypesCommand();
            var result = await mediator.Send(preloadTypes);
            return result.ToHttpResult();
        }

        private static async Task<IResult> BackfillSpriteImages(IMediator mediator)
        {
            var result = await mediator.Send(new BackfillSpriteImagesCommand());
            return result.ToHttpResult();
        }

        private static async Task<IResult> WarmUpDb(IMediator mediator)
        {
            await mediator.Send(new HealthQuery());
            return Results.Ok();
        }
    }
}
