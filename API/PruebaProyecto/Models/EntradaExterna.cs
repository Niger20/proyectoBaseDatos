using System.ComponentModel.DataAnnotations;

namespace PruebaProyecto.Models;

public class EntradaExterna
{
    [Key] public int IdEntradaExterna { get; set; }
    [Required] public int IdEntrada { get; set; }
    [Required] public int IdCompra { get; set; }
}