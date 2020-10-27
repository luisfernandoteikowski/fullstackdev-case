using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using GerenciadorEscolar.Entity;
namespace GerenciadorEscolar.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<GerenciadorEscolarDbContext>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
