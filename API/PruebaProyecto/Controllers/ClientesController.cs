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

            if (clientes.Count == 0) return NotFound(new { message = "No hay clientes registrados" });

            var response = new
            {
                Code = 200,
                Message = "Lista Ventas",
                Data = clientes
            };

            return Ok(response);
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
            var clientes = _context.Clientes.Find(id);

            if (clientes == null) return NotFound(new { message = "No existe cliente con tal codigo" });
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
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
            return Ok(new { message = "Cliente agregado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    //METODO PARA ACTUALIZAR Empleados

    [HttpPut("Editar")]
    public IActionResult Put(Clientes model)
    {
        if (model == null || model.IdCliente == 0)
        {
            if (model == null)
                return BadRequest(new { message = "Los datos no son validos" });
            if (model.IdCliente == 0) return BadRequest(new { message = "El codigo de cliente no es valido" });
        }

        try
        {
            var clientes = _context.Clientes.Find(model.IdCliente);

            if (clientes == null) return BadRequest(new { message = "No existe cliente con tal codigo" });

            clientes.Identidad = model.Identidad;
            clientes.PrimerNombre = model.PrimerNombre;
            clientes.SegundoNombre = model.SegundoNombre;
            clientes.PrimerApellido = model.PrimerApellido;
            clientes.SegundoApellido = model.SegundoApellido;
            clientes.Telefono = model.Telefono;


            _context.SaveChanges();
            return Ok(new { message = "Los datos del cliente se han actualizado" });
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
            var clientes = _context.Clientes.Find(id);

            if (clientes == null) return NotFound(new { message = "No existe cliente con ese codigo" });

            _context.Clientes.Remove(clientes);
            _context.SaveChanges();

            return Ok(new { message = "Registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
}