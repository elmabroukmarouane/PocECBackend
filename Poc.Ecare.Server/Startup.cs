using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poc.Ecare.Server.Extensions.Add;
using Poc.Ecare.Server.Extensions.Use;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Poc.Ecare.Server.RealTime.Hubs.Classe;

namespace Poc.Ecare.Server
{
    public class Startup
    {
        public IHostEnvironment _currentEnvironment { get; }
        public Startup(IConfiguration configuration, IHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration.GetSection("DatabaseType").Value == "ORACLE")
            {
                services.AddDbContext<DatabaseContext>(option =>
                    option.UseOracle(Configuration.GetConnectionString("OracleConnection"))
                );
            }
            else
            {
                services.AddDbContext<DatabaseContext>(option =>
                    option.UseSqlite(Configuration.GetConnectionString("SqliteConnection"))
                );
            }
            //services.AddOcelot(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddJWT(Configuration);
            services.AddCORS(Configuration);
            services.AddSERVICES(Configuration, _currentEnvironment);
            services.AddControllers()
                    .AddNewtonsoftJson(option => {
                        option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    });
            services.AddSWAGGER();
            services.AddRazorPages();
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecare GP POC v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseOcelot().Wait();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCORS(Configuration);
            app.UseJWT();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RealTimeHub>("/realtimehub");
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
