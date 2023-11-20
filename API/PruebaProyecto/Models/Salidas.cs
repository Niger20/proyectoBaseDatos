using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class Salidas
{

    [Key] public int IdSalida { get; set; }
    [Required] public int IdProducto { get; set; }
    [Required] public int IdCuarto { get; set; }
    [Required] public int IdEmpleado { get; set; }
    [Required] public DateTime Fecha { get; set; }
    [Required] public int Cantidad { get; set; }
    [Required] public string Tipo { get; set; }

}