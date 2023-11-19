using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class VistaVentasController : ControllerBase
{
    private readonly MyDBcontext _context;

    public VistaVentasController(MyDBcontext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetVista()
    {
        var vista = new List<VistaVentas>();

        try
        {
            using (var connection = new SqlConnection("server = LAPTOP-34RRR9F7 ; database = proyectoBaseDatos; integrated security = true; TrustServerCertificate = true")) // Reemplaza con tu cadena de conexión
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM VistaVentasConCliente", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vistaResult = new VistaVentas()
                        {
                            IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
                            IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                            NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                            IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                            ProductoDescripcion = reader.GetString(reader.GetOrdinal("ProductoDescripcion")),
                            Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                            Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                            Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                            Total = reader.GetDecimal(reader.GetOrdinal("Total"))
                        };

                        vista.Add(vistaResult);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            throw;
        }

        return Ok(vista);
    }



    
}