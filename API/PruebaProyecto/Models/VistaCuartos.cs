using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class VistaCuartos
{
    public int IdCuarto { get; set; }
    public int IdProducto { get; set;  }
    public string Descripcion { get; set;  }
    public int CantidadTotal { get; set;  }
    
}