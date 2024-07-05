using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpresaApi.Models;
using EmpresaApi.Services;

namespace EmpresaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados([FromQuery] string? nombre = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var empleados = await _empleadoService.GetEmpleados(nombre);
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleadoById(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoById(id);
            if (empleado == null)
                return NotFound();

            return Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> AddEmpleado(Empleado empleado)
        {
            var addedEmpleado = await _empleadoService.AddEmpleado(empleado);
            return CreatedAtAction(nameof(GetEmpleadoById), new { id = addedEmpleado.Id }, addedEmpleado);
        }
    }
}
