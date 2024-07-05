using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmpresaApi.Data;
using EmpresaApi.Models;
using System;
using System.Linq;

namespace EmpresaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Llamar al método SeedData para insertar datos en la base de datos
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EmpresaContext>();
                    SeedData(context);
                }
                catch (Exception ex)
                {
                    // Manejar errores al insertar datos en la base de datos
                    Console.WriteLine("Error al insertar datos en la base de datos: " + ex.Message);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SeedData(EmpresaContext context)
        {
            // Asegurarse de que la base de datos esté creada
            context.Database.EnsureCreated();

            // Verificar si ya hay datos en la tabla Empleados
            if (!context.Empleados.Any())
            {
                // Crear nuevos empleados
                var empleados = new[]
                {
                    new Empleado
                    {
                        Nombre = "Juan",
                        Apellido = "Pérez",
                        Correo = "juan.perez@example.com",
                        Telefono = "123456789",
                        Puesto = "Desarrollador"
                    },
                    new Empleado
                    {
                        Nombre = "María",
                        Apellido = "Gómez",
                        Correo = "maria.gomez@example.com",
                        Telefono = "987654321",
                        Puesto = "Diseñador"
                    },
                    new Empleado
                    {
                        Nombre = "Carlos",
                        Apellido = "López",
                        Correo = "carlos.lopez@example.com",
                        Telefono = "456789123",
                        Puesto = "Analista"
                    },
                    new Empleado
                    {
                        Nombre = "Ana",
                        Apellido = "Martínez",
                        Correo = "ana.martinez@example.com",
                        Telefono = "789123456",
                        Puesto = "Gerente"
                    },
                    new Empleado
                    {
                        Nombre = "Pedro",
                        Apellido = "Sánchez",
                        Correo = "pedro.sanchez@example.com",
                        Telefono = "159357456",
                        Puesto = "Programador"
                    },
                    new Empleado
                    {
                        Nombre = "Laura",
                        Apellido = "Hernández",
                        Correo = "laura.hernandez@example.com",
                        Telefono = "357159852",
                        Puesto = "Tester"
                    },
                    new Empleado
                    {
                        Nombre = "Miguel",
                        Apellido = "García",
                        Correo = "miguel.garcia@example.com",
                        Telefono = "456123789",
                        Puesto = "Ingeniero"
                    },
                    new Empleado
                    {
                        Nombre = "Sofía",
                        Apellido = "Díaz",
                        Correo = "sofia.diaz@example.com",
                        Telefono = "123789456",
                        Puesto = "Consultor"
                    },
                    new Empleado
                    {
                        Nombre = "Luis",
                        Apellido = "Torres",
                        Correo = "luis.torres@example.com",
                        Telefono = "789456123",
                        Puesto = "Administrador"
                    },
                    new Empleado
                    {
                        Nombre = "Elena",
                        Apellido = "Ruiz",
                        Correo = "elena.ruiz@example.com",
                        Telefono = "456789123",
                        Puesto = "Analista"
                    }
                };

                // Agregar los empleados a la base de datos
                context.Empleados.AddRange(empleados);
                context.SaveChanges();
            }
        }
    }
}
