using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ApiCsharp.Api.Controllers.Middlewares;
using ApiCsharp.Api.Controllers.Parsers;
using ApiCsharp.Api.Core.Application.ProductAgg.AppServices;
using ApiCsharp.Api.Core.Application.ProductAgg.Parsers;
using ApiCsharp.Api.Core.Domain.EstoqueAgg.Repositories;
using ApiCsharp.Api.Core.Domain.ProductAgg.Repositories;
using ApiCsharp.Api.Core.Domain.Shared.Repositories;
using ApiCsharp.Api.Core.Infrastructure.EstoqueAgg.Repositories;
using ApiCsharp.Api.Core.Infrastructure.ProductAgg.Repositories;
using ApiCsharp.Api.Core.Infrastructure.Shared;


namespace ApiCsharp.Api
{
    public class Startup
    
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddSeq());
            services.AddCors(options =>
            {
                options.AddPolicy("all", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Web com o ASP.NET Core", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "ApiCsharp.Api.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddDbContext<PedidoDbContext>(options =>
            {
                DbContextOptionsBuilder dbContextOptionsBuilder = options.UseSqlite(Configuration.GetConnectionString("Sqlite"));
            });

            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            services.AddScoped<ProdutoAppService>();
            services.AddScoped<ProdutoReportParser>();
            services.AddScoped<IProdutoParseFactory, ProdutoParseFactory>();
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<PedidoDbContext>());
            services.AddMediatR(Assembly.GetEntryAssembly());
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://tiagor87.auth0.com/";
                options.Audience = "https://pedido-api.unifeso-poo.com.br";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PedidoDbContext dbContext, ILogger<Startup> logger, IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                logger.LogInformation("Application started");
            });
            
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Application stopping");
            });
            
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                logger.LogInformation("Application stopped");
            });
            
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCsharp.Api v1"));
            }

            app.UseCors("all");
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<LoggerHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapControllers()
                    .RequireAuthorization();
            });
        }
    }
}
