using CajaAhorro.Application.Interfaces;
using CajaAhorro.Application.Services;
using CajaAhorro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Utilities;

public static class DependencyInjection
{
    public const string ConnectionString = "Data Source=caja_ahorro.db";

    public static void InjectDependencies(this WebApplicationBuilder builder)
    { 
        builder.InjectCors();
        builder.InjectContext();
        builder.InjectServices();
    }

    // aqui vas a registrar los contextos de base de datos que vas a usar en tu proyecto
    public static void InjectContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AhorroDbContext>(options =>
        {
            options.UseSqlite(ConnectionString);
        });
    }

    public static void InjectServices(this WebApplicationBuilder builder)
    {
        // aqui vas a registrar los servicios que vas a usar en tu proyecto
        // ejemplo: builder.Services.AddScoped<IMiServicio, MiServicio>();

        builder.Services.AddScoped<IAhorroService, AhorroService>();
        builder.Services.AddScoped<ISocioService, SocioService>();
    }

    public static void InjectCors(this WebApplicationBuilder builder)
    {
        // aqui vas a registrar los cors que vas a usar en tu proyecto
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowElectron",
                policy => policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });
    }

    // este metodo se encarga de migrar la base de datos al iniciar la aplicacion
    public static void MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AhorroDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
