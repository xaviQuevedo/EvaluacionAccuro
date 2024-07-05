using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using EmpresaApi.Data;
using EmpresaApi.Services;

namespace EmpresaApi
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
            services.AddControllers();
            
            // Configuración de DbContext para Entity Framework Core con SQLite
            services.AddDbContext<EmpresaContext>(options =>
                options.UseSqlite("Data Source=empresa.db"));

            // Registro de servicios
            services.AddScoped<IEmpleadoService, EmpleadoService>();

            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmpresaApi", Version = "v1" });
            });

            // Configuración de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmpresaApi v1"));
            }

            // Middleware para manejar solicitudes HTTP
            app.UseRouting();

            // Aplicar la política CORS
            app.UseCors("AllowOrigin");

            // Middleware para autorización
            app.UseAuthorization();

            // Middleware para enrutamiento de Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Creación de la base de datos SQLite si no existe
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var context = serviceScope?.ServiceProvider.GetRequiredService<EmpresaContext>();
                context?.Database.EnsureCreated();
            }
        }
    }
}
