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

public class VistaCuartosController : ControllerBase
{
    private readonly MyDBcontext _context;

    public VistaCuartosController(MyDBcontext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetVista()
    {
        var vista = new List<VistaCuartos>();

        try
        {
            using (var connection = new SqlConnection("server = LAPTOP-RH59HPEB ; database = proyectoBaseDatos; integrated security = true; TrustServerCertificate = true")) // Reemplaza con tu cadena de conexión
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM ProductosPorCuarto", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vistaResult = new VistaCuartos()
                        {
                            IdCuarto = reader.GetInt32(reader.GetOrdinal("IdCuarto")),
                            IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            CantidadTotal = reader.GetInt32(reader.GetOrdinal("CantidadTotal")),
                        };

                        vista.Add(vistaResult);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }

        var response = new 
        {
            Code = 200,
            Message = "Reporte Cuartos",
            Data = vista
        };
                
                
        return Ok(response);
    }

}