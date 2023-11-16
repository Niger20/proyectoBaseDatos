namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class ProveedorController : ControllerBase
{
    private readonly MyDBcontext _context;

    public ProveedorController(MyDBcontext context)
    {
        _context = context;
    }


    //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var proveedores = _context.Proveedores.ToList();

            if (proveedores.Count == 0) return NotFound("No hay proveedores Registrados");

            return Ok(proveedores);
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
            var proveedores = _context.Proveedores.Find(id);

            if (proveedores == null) return NotFound($"No hay proveedores con codigo: {id}");
            return Ok(proveedores);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA CREAR Empleados

    [HttpPost( "Crear")]
    public IActionResult Post(Proveedores model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("proveedor Agregado Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA ACTUALIZAR Empleados

    [HttpPut("Editar")]
    public IActionResult Put(Proveedores model)
    {
        if (model == null || model.IdProveedor == 0)
        {
            if (model == null)
                return BadRequest("El modelo de datos no es valido");
            if (model.IdProveedor == 0) return BadRequest($"El codigo de proveedor {model.IdProveedor} no es valido");
        }

        try
        {
            var proveedores = _context.Proveedores.Find(model.IdProveedor);

            if (proveedores == null) return BadRequest($"El codigo de proveedor {model.IdProveedor} no es valido");

            proveedores.Identidad = model.Identidad;
            proveedores.PrimerNombre = model.PrimerNombre;
            proveedores.SegundoNombre = model.SegundoNombre;
            proveedores.PrimerApellido = model.PrimerApellido;
            proveedores.SegundoApellido = model.SegundoApellido;


            _context.SaveChanges();
            return Ok("Los detalles del proveedor se han actualizado");
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
            var proveedores = _context.Proveedores.Find(id);

            if (proveedores == null) return NotFound($"No existe proveedor con codigo {id}");

            _context.Proveedores.Remove(proveedores);
            _context.SaveChanges();

            return Ok("Registro eliminado correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}