using Estacionamento.Infra.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Estacionamento.Domain.DomainObjetcs.Messsages.CommonMessages.Notifications;
using Estacionamento.Domain.Messsages.CommonMessages.Notifications;
using Estacionamento.Infra.Repositories;
using Estacionamento.Application.Commands.EntregaVeiculo;
using Estacionamento.Domain.Interfaces.Repositories;
using Estacionamento.Application.Commands.SaidaVeiculo;

namespace Estacionamento.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EstacionamentoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));



            services.ConfigureAudit(configuration);

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
    }
}
