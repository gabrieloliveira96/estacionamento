using Estacionamento.WebApi.Configurations;
using Estacionamento.WebApi.Middlewares;
using MediatR;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
namespace Estacionament.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IWebHostEnvironment env, IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "EstacionamentoCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddControllers();

            services.AddMediatR(typeof(Program));

            services
                .AddNotifications()
                .AddRepositories()
                .AddCommands()
                .RegisterServices(Configuration);



            services.AddControllers();

            services.AddLogging();


            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceCollection services)
        {

            app.UseSwagger(Configuration);
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors("EstacionamentoCorsPolicy");

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
