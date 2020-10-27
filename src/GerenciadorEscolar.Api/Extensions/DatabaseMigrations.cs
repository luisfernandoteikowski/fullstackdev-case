using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GerenciadorEscolar.Api
{
    public static class DatabaseMigrations
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ocorreu um erro aos executar migrations no banco de dados.");
                }
            }
            return host;
        }
    }
}