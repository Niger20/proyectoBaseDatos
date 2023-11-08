using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PruebaProyecto.Models
{
    public class Empleados
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required]
        public string Identidad { get; set; }

        [Required]
        public string PrimerNombre { get; set;}
        
        public string SegundoNombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }
        
        public string SegundoApellido { get; set;}

    }
}
