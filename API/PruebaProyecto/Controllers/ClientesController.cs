namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class ClientesController : ControllerBase
{
    private readonly MyDBcontext _context;

    public ClientesController(MyDBcontext context)
    {
        _context = context;
    }


    //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var clientes = _context.Clientes.ToList();

            if (clientes.Count == 0) return NotFound("No hay Clientes Registrados");

            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // METODO PARA LEER LOS EMPLEADOS SEGUN SU ID

    [HttpGet("Buscar")]
    public IActionResult Get(int id)
    {
        try
        {
            var clientes = _context.Clientes.Find(id);

            if (clientes == null) return NotFound($"No hay clientes con codigo: {id}");
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA CREAR Empleados

    [HttpPost("Crear")]
    public IActionResult Post(Clientes model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("Cliente Agregado Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA ACTUALIZAR Empleados

    [HttpPut("Editar")]
    public IActionResult Put(Clientes model)
    {
        if (model == null || model.IdCliente == 0)
        {
            if (model == null)
                return BadRequest("El modelo de datos no es valido");
            if (model.IdCliente == 0) return BadRequest($"El codigo de cliente {model.IdCliente} no es valido");
        }

        try
        {
            var clientes = _context.Clientes.Find(model.IdCliente);

            if (clientes == null) return BadRequest($"El codigo de cliente {model.IdCliente} no es valido");

            clientes.Identidad = model.Identidad;
            clientes.PrimerNombre = model.PrimerNombre;
            clientes.SegundoNombre = model.SegundoNombre;
            clientes.PrimerApellido = model.PrimerApellido;
            clientes.SegundoApellido = model.SegundoApellido;
            clientes.Telefono = model.Telefono;


            _context.SaveChanges();
            return Ok("Los detalles del Cliente se han actualizado");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA ELIMINAR UN EMPLEADO

    [HttpDelete("Eliminar")]
    public IActionResult Delete(int id)
    {
        try
        {
            var clientes = _context.Clientes.Find(id);

            if (clientes == null) return NotFound($"No existe cliente con codigo {id}");

            _context.Clientes.Remove(clientes);
            _context.SaveChanges();

            return Ok("Registro eliminado correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}