using Microsoft.EntityFrameworkCore;
using EmpresaApi.Models;

namespace EmpresaApi.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
    }
}
