using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using System.Net;

namespace ShoppingAPI.Middlewares
{
    public class GlobalExceptonHandlingMiddlewares
    {
        private readonly ILogger<GlobalExceptonHandlingMiddlewares> logger;
        private readonly RequestDelegate next;
        public GlobalExceptonHandlingMiddlewares(ILogger<GlobalExceptonHandlingMiddlewares> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                var resultApi = new ResponseApi
                {
                    Message = new[] { "Something error" },
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false
                }.ToString();

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(resultApi);

            }
        }
    }
}
