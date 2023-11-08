using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PruebaProyecto.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Peso { get; set; }
        

    }
}
