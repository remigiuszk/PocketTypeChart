using Application.PokeTypes.GetAllTypes;
using Application.PokeTypes.PreloadTypes;
using MediatR;
using PocketTypeChart.Extensions.Application;

namespace PocketTypeChart.Endpoints
{
    public static class PokeTypeEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var posts = app.MapGroup("/api/poketypes");

            posts.MapGet("/", GetAllPokeTypes);
            posts.MapPost("/preloadTypes", PreloadPokeTypes);
        }

        private static async Task<IResult> GetAllPokeTypes(IMediator mediator)
        {
            var getAllTypes = new GetAllTypesQuery();
            var result = await mediator.Send(getAllTypes);
            return result.ToHttpResult();
        }

        private static async Task<IResult> PreloadPokeTypes(IMediator mediator)
        {
            var preloadTypes = new PreloadTypesCommand();
            var result = await mediator.Send(preloadTypes);
            return result.ToHttpResult();
        }
    }
}
