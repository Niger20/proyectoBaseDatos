using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class Compras
{

    [Key] public int IdCompra { get; set; }
    [Required] public int IdProveedor { get; set; }
    [Required] public int IdProducto { get; set; }
    [Required] public decimal Precio { get; set; }
    [Required] public DateTime Fecha { get; set; }
    [Required] public int Cantidad { get; set; }
    [NotMapped] public decimal Total => Precio * Cantidad;

}