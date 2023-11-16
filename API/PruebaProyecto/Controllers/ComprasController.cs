namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class ComprasController : ControllerBase
{
    private readonly MyDBcontext _context;

    public ComprasController(MyDBcontext context)
    {
        _context = context;
    }


    //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var compras = _context.Compras.ToList();

            if (compras.Count == 0) return NotFound("No hay Compras Registrados");

            return Ok(compras);
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
            var compras = _context.Compras.Find(id);

            if (compras == null) return NotFound($"No hay compras con codigo: {id}");
            return Ok(compras);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA CREAR Empleados

    [HttpPost("Crear")]
    public IActionResult Post(Compras model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("compra Agregado Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //METODO PARA ACTUALIZAR Empleados

    [HttpPut("Editar")]
    public IActionResult Put(Compras model)
    {
        if (model == null || model.IdCompra == 0)
        {
            if (model == null)
                return BadRequest("El modelo de datos no es valido");
            if (model.IdCompra == 0) return BadRequest($"El codigo de compra {model.IdCompra} no es valido");
        }

        try
        {
            var compras = _context.Compras.Find(model.IdCompra);

            if (compras == null) return BadRequest($"El codigo de compra {model.IdCompra} no es valido");

            compras.IdProveedor = model.IdProveedor;
            compras.IdProducto = model.IdProducto;
            compras.Precio = model.Precio;
            compras.Fecha = model.Fecha;
            compras.Cantidad = model.Cantidad;


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
            var compras = _context.Compras.Find(id);

            if (compras == null) return NotFound($"No existe compra con codigo {id}");

            _context.Compras.Remove(compras);
            _context.SaveChanges();

            return Ok("Registro eliminado correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}