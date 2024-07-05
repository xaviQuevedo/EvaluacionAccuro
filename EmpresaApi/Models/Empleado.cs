using System.ComponentModel.DataAnnotations;

namespace EmpresaApi.Models
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string? Correo { get; set; }

        [Required]
        [Phone]
        public string? Telefono { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Puesto { get; set; }
    }
}
