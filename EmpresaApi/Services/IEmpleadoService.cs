using System.Collections.Generic;
using System.Threading.Tasks;
using EmpresaApi.Models;

namespace EmpresaApi.Services
{
    public interface IEmpleadoService
    {
        Task<Empleado> AddEmpleado(Empleado empleado);
        Task<IEnumerable<Empleado>> GetEmpleados(string? nombre = null);
        Task<Empleado> GetEmpleadoById(int id);
        Task<bool> DeleteEmpleado(int id);
    }
}

