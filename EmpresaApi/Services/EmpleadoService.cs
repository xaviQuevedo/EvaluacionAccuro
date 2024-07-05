using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmpresaApi.Data;
using EmpresaApi.Models;

namespace EmpresaApi.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly EmpresaContext _context;

        public EmpleadoService(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Empleado> AddEmpleado(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<IEnumerable<Empleado>> GetEmpleados(string? nombre = null)
        {
            IQueryable<Empleado> query = _context.Empleados;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre != null && e.Nombre.Contains(nombre));
            }

            query = query.OrderBy(e => e.Id); // Ordenar por defecto, puedes cambiar el campo según tus necesidades

            return await query
                .ToListAsync();
        }

        public async Task<Empleado> GetEmpleadoById(int id)
        {
            // Cambiamos la firma para que coincida con la interfaz
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return await _context.Empleados.FindAsync(id);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }


        public async Task<bool> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return false;

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


