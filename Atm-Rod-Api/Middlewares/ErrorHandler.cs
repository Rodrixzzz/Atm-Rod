using Atm_Rod_Entities.Exceptions;
using Atm_Rod_Entities.Response;
using System.Net;
using System.Text.Json;

namespace Atm_Rod_Api.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new CustomResponse<string>(false, e.Message);
                switch (e)
                {
                    case CustomException ex:
                        response.StatusCode = (int)ex.statusCode;
                        responseModel.Errors = new List<string>() { ex.Message };
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
