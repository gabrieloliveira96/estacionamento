using Estacionamento.Infra.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using Estacionamento.Infra.Repositories;
using Estacionamento.Application.Commands.EntregaVeiculo;
using Estacionamento.Domain.Interfaces.Repositories;
using Estacionamento.Application.Commands.SaidaVeiculo;
using Estacionamento.Domain.Interfaces.Queries;
using Estacionamento.Application.Queries.Vagas;
using Estacionamento.Application.Queries.RegistroVeiculos;

namespace Estacionamento.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EstacionamentoContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<EstacionamentoContext>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services.AddApiAuthentication(configuration);
            services.AddAuthorizationPolicies();
            services.AddSwagger(configuration);

        }
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<DomainSuccesNotification>, DomainSuccesNotificationHandler>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryRegistroVeiculo, RepositoryRegistroVeiculo>();
            services.AddScoped<IRepositoryVaga, RepositoryVaga>();

            return services;
        }
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<EntregaVeiculoCommand, Unit>, EntregaVeiculoCommandHandler>();
            services.AddScoped<IRequestHandler<SaidaVeiculoCommand, Unit>, SaidaVeiculoCommandHandler>();

            return services;
        }
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IVagasQuery, VagasQuery>();
            services.AddScoped<IRegistroVeiculosQuery, RegistroVeiculosQuery>();
            return services;
        }
    }
}
