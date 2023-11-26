namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;



[Route("api/[controller]")]
[ApiController]

public class SalidaExternaController : ControllerBase
{
    private readonly MyDBcontext _context;

    public SalidaExternaController(MyDBcontext context)
    {
        _context = context;
    }

    //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var salidaExterna = _context.SalidaExterna.ToList();

            if (salidaExterna.Count == 0)
            {
                return NotFound(new { message = "No hay salidaExterna Registrados" });
            }

            return Ok(salidaExterna);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

// METODO PARA LEER LOS EMPLEADOS SEGUN SU ID
    [HttpGet("Buscar")]
    public IActionResult Get(int id)
    {
        try
        {
            var salidaExterna = _context.SalidaExterna.Find(id);

            if (salidaExterna == null)
            {
                return NotFound(new { message = $"No hay salidaExterna con codigo: {id}" });
            }

            return Ok(salidaExterna);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA CREAR Empleados
    [HttpPost("Crear")]
    public IActionResult Post(SalidaExterna model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok(new { message = "salidaExterna Agregado Correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA ACTUALIZAR Empleados
    [HttpPut("Editar")]
    public IActionResult Put(SalidaExterna model)
    {
        if (model == null || model.IdSalidaExterna == 0)
        {
            if (model == null)
            {
                return BadRequest(new { message = "El modelo de datos no es valido" });
            }
            else if (model.IdSalidaExterna == 0)
            {
                return BadRequest(new
                    { message = $"El codigo de entradaExterna {model.IdSalidaExterna} no es valido" });
            }
        }

        try
        {
            var salidaExterna = _context.SalidaExterna.Find(model.IdSalidaExterna);

            if (salidaExterna == null)
            {
                return BadRequest(new { message = $"El codigo de salidaExterna {model.IdSalidaExterna} no es valido" });
            }

            salidaExterna.IdSalida = model.IdSalida;
            salidaExterna.IdVenta = model.IdVenta;

            _context.SaveChanges();
            return Ok(new { message = "Los detalles de la venta se han actualizado" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA ELIMINAR UN EMPLEADO
    [HttpDelete("Eliminar")]
    public IActionResult Delete(int id)
    {
        try
        {
            var salidaExterna = _context.SalidaExterna.Find(id);

            if (salidaExterna == null)
            {
                return NotFound(new { message = $"No existe salidaExterna con codigo {id}" });
            }

            _context.SalidaExterna.Remove(salidaExterna);
            _context.SaveChanges();

            return Ok(new { message = "Registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



    
}