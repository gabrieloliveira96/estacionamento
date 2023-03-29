using Estacionamento.Domain.Constants;
using System.Net;
using System.Text.Json;

namespace Estacionamento.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                response.StatusCode = (int)HttpStatusCode.BadRequest;

                var result = JsonSerializer.Serialize(new
                {
                    success = false,
                    errors = new List<string> { MensagemErrosContants.MENSAGEM_GENERICA_ERRO },
                });

                _logger.LogError(error, error?.Message);

                await response.WriteAsync(result);
            }
        }
    }

}
