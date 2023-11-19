using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PruebaProyecto.Models;

public class VistaCompras
{
    public int IdCompra { get; set; }
    public string NombreProveedor { get; set;  }
    public string ProductoDescripcion { get; set;  }
    public decimal Precio { get; set;  }
    public DateTime Fecha { get; set;  }
    public int Cantidad { get; set;  }
    public decimal Total { get; set;  }
    
    
}