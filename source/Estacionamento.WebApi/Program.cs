using Estacionament.WebApi;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var startup = new Startup(builder.Configuration);

    startup.ConfigureServices(builder.Environment, builder.Services);

    var app = builder.Build();

    startup.Configure(app, app.Environment, builder.Services);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro ao inciar aplicação");
    throw;
}
finally
{
    Log.Information("Derrubando aplicação");
}
   