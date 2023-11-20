using System.ComponentModel.DataAnnotations;

namespace PruebaProyecto.Models;

public class MovimientoInterno
{
    [Key] public int IdMovimientoInterno { get; set; }
    [Required] public int IdSalida { get; set; }
    [Required] public int IdEntrada { get; set; }
}