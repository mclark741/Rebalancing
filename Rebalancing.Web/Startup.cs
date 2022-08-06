using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rebalancing.Core;
using Rebalancing.Data;
using Rebalancing.Data.Repositories;
using Rebalancing.Import;
using Rebalancing.Integrations;

namespace Rebalancing.Web
{
    public class Startup
    {
        private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Entity Framework
            services.AddDbContextPool<RebalancingDbContext>(options =>
               options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddHttpClient<RapidApiYahooFinanceClient>();

            services.AddScoped<IMarket, Market>();
            services.AddScoped<IPortfolio, Portfolio>();

            services.AddScoped<IFileImport, FidelityCsvImporter>();

            services.AddLogging();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rebalancing.Web", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rebalancing.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_myAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //// NOTE: this must go at the end of Configure
            //var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using var serviceScope = serviceScopeFactory.CreateScope();
            //var dbContext = serviceScope.ServiceProvider.GetService<RebalancingDbContext>();
            //dbContext.Database.EnsureCreated();
        }
    }
}
