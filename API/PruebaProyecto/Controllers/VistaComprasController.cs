using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class VistaComprasController : ControllerBase
{
    private readonly MyDBcontext _context;

    public VistaComprasController(MyDBcontext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetVista()
    {
        var vista = new List<VistaCompras>();

        try
        {
            using (var connection = new SqlConnection("server = LAPTOP-34RRR9F7 ; database = proyectoBaseDatos; integrated security = true; TrustServerCertificate = true")) // Reemplaza con tu cadena de conexión
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM VistaComprasConProveedor", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vistaResult = new VistaCompras()
                        {
                            IdCompra = reader.GetInt32(reader.GetOrdinal("IdCompra")),
                            NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
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
    
    [HttpPost("Filtrar")]
    public IActionResult GetVistaFiltrada(DateTime fechaInicio, DateTime fechaFinal)
    {
        var vista = new List<VistaCompras>();

        try
        {
            using (var connection = new SqlConnection("server = LAPTOP-34RRR9F7 ; database = proyectoBaseDatos; integrated security = true; TrustServerCertificate = true")) // Reemplaza con tu cadena de conexión
            {
                connection.Open();

                using (var command = new SqlCommand($"SELECT * FROM VistaComprasConProveedor WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFinal}'", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vistaResult = new VistaCompras()
                        {
                            IdCompra = reader.GetInt32(reader.GetOrdinal("IdCompra")),
                            NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
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