namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class EntradaExternaController : ControllerBase
{
    private readonly MyDBcontext _context;

    public EntradaExternaController(MyDBcontext context)
    {
        _context = context;
    }

//  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var entradaExterna = _context.EntradaExterna.ToList();

            if (entradaExterna.Count == 0)
            {
                return NotFound(new { message = "No hay entradaExterna Registrados" });
            }

            return Ok(entradaExterna);
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
            var entradaExterna = _context.EntradaExterna.Find(id);

            if (entradaExterna == null)
            {
                return NotFound(new { message = $"No hay entradaExterna con codigo: {id}" });
            }

            return Ok(entradaExterna);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA CREAR Empleados
    [HttpPost("Crear")]
    public IActionResult Post(EntradaExterna model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok(new { message = "entradaExterna Agregado Correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA ACTUALIZAR Empleados
    [HttpPut("Editar")]
    public IActionResult Put(EntradaExterna model)
    {
        if (model == null || model.IdEntradaExterna == 0)
        {
            if (model == null)
            {
                return BadRequest(new { message = "El modelo de datos no es valido" });
            }
            else if (model.IdEntradaExterna == 0)
            {
                return BadRequest(
                    new { message = $"El codigo de entradaExterna {model.IdEntradaExterna} no es valido" });
            }
        }

        try
        {
            var entradaExterna = _context.EntradaExterna.Find(model.IdEntradaExterna);

            if (entradaExterna == null)
            {
                return BadRequest(new { message = $"El codigo de venta {model.IdEntradaExterna} no es valido" });
            }

            entradaExterna.IdEntrada = model.IdEntrada;
            entradaExterna.IdCompra = model.IdCompra;

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
            var entradaExterna = _context.EntradaExterna.Find(id);

            if (entradaExterna == null)
            {
                return NotFound(new { message = $"No existe entradaExterna con codigo {id}" });
            }

            _context.EntradaExterna.Remove(entradaExterna);
            _context.SaveChanges();

            return Ok(new { message = "Registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    
}