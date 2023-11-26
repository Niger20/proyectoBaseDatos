namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;



[Route("api/[controller]")]
[ApiController]

public class MovimientoInternoController : ControllerBase
{
    private readonly MyDBcontext _context;

    public MovimientoInternoController(MyDBcontext context)
    {
        _context = context;
    }

    //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var movimientoInterno = _context.MovimientoInterno.ToList();

            if (movimientoInterno.Count == 0)
            {
                return NotFound(new { message = "No hay movimientoInterno Registrados" });
            }

            return Ok(movimientoInterno);
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
            var movimientoInterno = _context.MovimientoInterno.Find(id);

            if (movimientoInterno == null)
            {
                return NotFound(new { message = $"No hay movimientoInterno con codigo: {id}" });
            }

            return Ok(movimientoInterno);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA CREAR SALIDAS
    [HttpPost("Crear")]
    public IActionResult Post(MovimientoInterno model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok(new { message = "movimientoInterno Agregado Correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA ACTUALIZAR Empleados
    [HttpPut("Editar")]
    public IActionResult Put(MovimientoInterno model)
    {
        if (model == null || model.IdMovimientoInterno == 0)
        {
            if (model == null)
            {
                return BadRequest(new { message = "El modelo de datos no es valido" });
            }
            else if (model.IdMovimientoInterno == 0)
            {
                return BadRequest(new
                    { message = $"El codigo de entradaExterna {model.IdMovimientoInterno} no es valido" });
            }
        }

        try
        {
            var movimientoInterno = _context.MovimientoInterno.Find(model.IdMovimientoInterno);

            if (movimientoInterno == null)
            {
                return BadRequest(new
                    { message = $"El codigo de movimientoInterno {model.IdMovimientoInterno} no es valido" });
            }

            movimientoInterno.IdSalida = model.IdSalida;
            movimientoInterno.IdEntrada = model.IdEntrada;

            _context.SaveChanges();
            return Ok(new { message = "Los detalles de la movimientoInterno se han actualizado" });
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
            var movimientoInterno = _context.MovimientoInterno.Find(id);

            if (movimientoInterno == null)
            {
                return NotFound(new { message = $"No existe movimientoInterno con codigo {id}" });
            }

            _context.MovimientoInterno.Remove(movimientoInterno);
            _context.SaveChanges();

            return Ok(new { message = "Registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



    
}