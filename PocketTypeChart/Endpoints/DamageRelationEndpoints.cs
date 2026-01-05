using Application.DamageRelations.GetTypingEffectivenessQuery;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PocketTypeChart.Extensions.Application;

namespace PocketTypeChart.Endpoints
{
    public static class DamageRelationEndpoints
    {
        public static void RegisterDamageRelationEndpoints(this WebApplication app)
        {
            var posts = app.MapGroup("/api/damagerelations").RequireRateLimiting("global");

            posts.MapGet("/", GetTypingEffectiveness);
        }

        private static async Task<IResult> GetTypingEffectiveness(
            string selectedTypesId,
            IMediator mediator,
            IMemoryCache cache)
        {
            var ids = selectedTypesId.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            if (ids.Count == 0)
                return Results.BadRequest("selectedTypesId is required (e.g. 1,4).");

            var cacheKey = $"typing_effectiveness:{string.Join("-", ids)}";

            var model = await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                entry.Priority = CacheItemPriority.Normal;

                var res = await mediator.Send(new GetTypingEffectivenessQuery(ids));
                if (res.IsFailure) return null;
                return res.Value;
            });

            if (model is null)
                return Results.Problem("Failed to calculate typing effectiveness.");

            return Results.Ok(model);
        }
    }
}
