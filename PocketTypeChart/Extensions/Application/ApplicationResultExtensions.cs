using Application.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PocketTypeChart.Extensions.Application
{
    public static class ApplicationResultExtensions
    {
        public static IResult ToHttpResult(this Result result)
        {
            if (result.IsSuccess)
                return Results.NoContent();

            return HandleErrors(result.Error);
        }

        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return TypedResults.Ok(result.Value);

            return HandleErrors(result.Error);
        }

        private static ProblemHttpResult HandleErrors(Error error)
        {
            var problemDetails = CreateProblemDetails(error);

            return TypedResults.Problem(problemDetails);
        }

        private static ProblemDetails CreateProblemDetails(Error error)
        {
            return new ProblemDetails
            {
                Status = 400,
                Title = error.Code,
                Detail = error.Description
            };
        }
    }
}
