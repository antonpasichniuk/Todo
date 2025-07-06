using Microsoft.AspNetCore.Mvc;
using Todo.Common.Result;

namespace Todo.Web.Infrastructure
{
    public static class ResultExtension
    {
        public static IActionResult ToHttpResult<TValue>(this Result<TValue> result, int successStatusCode = 200)
        {
            if (result.IsSuccess)
            {
                return successStatusCode switch
                {
                    200 => new OkObjectResult(result.Value),
                    201 => new CreatedResult(string.Empty, result.Value),
                    _ => new ObjectResult(result.Value),
                };
            }

            return MapErrorToResult(result.Error!);
        }

        private static ObjectResult MapErrorToResult(Error error)
        {
            var problemDetails = error.Type switch
            {
                ErrorType.Failure => CreateProblemDetails(error.Message, 500),
                ErrorType.Validation => CreateProblemDetails(error.Message, 400),
                ErrorType.NotFound => CreateProblemDetails(error.Message, 404),
                ErrorType.Authorization => CreateProblemDetails(error.Message, 403),
                _ => CreateProblemDetails(string.Empty, 500)
            };

            return new ObjectResult(problemDetails);
        }

        private static ProblemDetails CreateProblemDetails(string errorMessage, int code)
        {
            return new ProblemDetails
            {
                Status = code,
                Title = errorMessage,
            };
        }
    }
}
