using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicVoting.Infrastructure;
using ElectronicVoting.Persistence;
using ElectronicVoting.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaillierCryptoSystem;
using PaillierCryptoSystem.Model;

namespace ElectronicVoting.Api
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
            KeyBuilder keyBuilder = new KeyBuilder();
            keyBuilder.Generate(80,100);
            
            KeyPrivate keyPrivate = keyBuilder.KeyPrivate;
            KeyPublic keyPublic = keyBuilder.KeyPublic;

            services.AddSingleton<KeyPublic>(keyPublic);
            services.AddSingleton<KeyPrivate>(keyPrivate);

            services.AddSignalRHubs();
            services.AddPersistence();
            services.AddServiceToken(Configuration);
            services.AddInfrastructure();
            services.AddControllersWithViews();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ElectronicVoting.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElectronicVoting.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}