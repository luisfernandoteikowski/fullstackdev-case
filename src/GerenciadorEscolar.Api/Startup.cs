using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GerenciadorEscolar.Entity;
using Microsoft.EntityFrameworkCore;
using GerenciadorEscolar.Api.Repository;
using GerenciadorEscolar.Api.Service;
using GerenciadorEscolar.Api.Filters;
namespace GerenciadorEscolar.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc((options) =>
            {
                options.Filters.Add(typeof(HttpResponseExceptionFilter));
            });

            services.AddDbContext<GerenciadorEscolarDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("App")));

            services.AddTransient<IEscolaRepository, EscolaRepository>();
            services.AddTransient<ITurmaRepository, TurmaRepository>();
            services.AddTransient<IEscolaService, EscolaService>();
            services.AddTransient<ITurmaService, TurmaService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
