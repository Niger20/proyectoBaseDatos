using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class Usuarios
{

    [Key] 
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    public string Rol { get; set;}
    
}