using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class Proveedores
{

    [Key] 
    public int IdProveedor { get; set; }
    
    [Required]
    public string Identidad { get; set; }
    
    [Required]
    public string PrimerNombre { get; set;}
        
    public string SegundoNombre { get; set; }

    [Required]
    public string PrimerApellido { get; set; }
        
    public string SegundoApellido { get; set;}

}