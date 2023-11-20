using System.ComponentModel.DataAnnotations;

namespace PruebaProyecto.Models;

public class SalidaExterna
{
    [Key] public int IdSalidaExterna { get; set; }
    [Required] public int IdSalida { get; set; }
    [Required] public int IdVenta { get; set; }
}