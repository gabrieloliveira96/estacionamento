
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Estacionamento.Infra.Contexts
{
    public static class EstacionamentoContextExtesionMethods
    {
        public static void ConfigureAudit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
        }

    }
}
