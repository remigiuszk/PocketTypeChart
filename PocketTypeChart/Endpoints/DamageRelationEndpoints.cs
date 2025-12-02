using Application.DamageRelations.GetTypingEffectivenessQuery;
using MediatR;
using PocketTypeChart.Extensions.Application;

namespace PocketTypeChart.Endpoints
{
    public static class DamageRelationEndpoints
    {
        public static void RegisterDamageRelationEndpoints(this WebApplication app)
        {
            var posts = app.MapGroup("/api/damagerelations");

            posts.MapGet("/", GetTypingEffectiveness);
        }

        private static async Task<IResult> GetTypingEffectiveness(string selectedTypesId, IMediator mediator)
        {
            var ids = selectedTypesId.Split(',')
                             .Select(int.Parse)
                             .ToList();

            var getTypingEffectiveness = new GetTypingEffectivenessQuery(ids);
            var result = await mediator.Send(getTypingEffectiveness);
            return result.ToHttpResult();
        }
    }
}
