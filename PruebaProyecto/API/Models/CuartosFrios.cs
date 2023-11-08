using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class CuartosFrios
{
    [Key]
    public int IdCuarto { get; set; }
    
    [Required]
    public int CapacidadMaxima { get; set; }
    
    [Required]
    public int CantidadActual { get; set; }
    
    [NotMapped] // Indica que este campo no está mapeado a una columna de la base de datos.
    public decimal CapacidadDisponible => CapacidadMaxima - CantidadActual;
    
    
    
    
    
}