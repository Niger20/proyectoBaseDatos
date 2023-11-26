using Microsoft.AspNetCore.Http.HttpResults;

namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class VentasController : ControllerBase
{
    private static MyDBcontext _context;
    

    public VentasController(MyDBcontext context)
    {
        _context = context;
    }


//  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        {
            var ventas = _context.Ventas.ToList();

            if (ventas.Count == 0)
            {
                return NotFound(new { message = "No hay ventas Registrados" });
            }

            var response = new
            {
                Code = 200,
                Message = "Lista Ventas",
                Data = ventas
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
            var ventas = _context.Ventas.Find(id);

            if (ventas == null)
            {
                return NotFound(new { message = $"No hay ventas con codigo: {id}" });
            }

            return Ok(ventas);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA CREAR Empleados
    [HttpPost("Crear")]
    public IActionResult Post(Ventas model)
    {
        try
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok(new { message = "compra Agregado Correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

//METODO PARA ACTUALIZAR Empleados
    [HttpPut("Editar")]
    public IActionResult Put(Ventas model)
    {
        if (model == null || model.IdVenta == 0)
        {
            if (model == null)
            {
                return BadRequest(new { message = "El modelo de datos no es valido" });
            }
            else if (model.IdVenta == 0)
            {
                return BadRequest(new { message = $"El codigo de venta {model.IdVenta} no es valido" });
            }
        }

        try
        {
            var ventas = _context.Ventas.Find(model.IdVenta);

            if (ventas == null)
            {
                return BadRequest(new { message = $"El codigo de venta {model.IdVenta} no es valido" });
            }

            ventas.IdCliente = model.IdCliente;
            ventas.IdProducto = model.IdProducto;
            ventas.Precio = model.Precio;
            ventas.Fecha = model.Fecha;
            ventas.Cantidad = model.Cantidad;

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
            var ventas = _context.Ventas.Find(id);

            if (ventas == null)
            {
                return NotFound(new { message = $"No existe venta con codigo {id}" });
            }

            _context.Ventas.Remove(ventas);
            _context.SaveChanges();

            return Ok(new { message = "Registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    
    [HttpPut("ActualizarCantidadVenta")]
    //METODO PARA ACTUALIZAR CANTIDAD DE productos venta
    public Boolean ActualizarCantidadVenta(int IdProducto, int Cantidad)
    {

        try
        {
            var productos = _context.Productos.Find(IdProducto);

            if (productos == null)
            {
                return false;
            }

            if (productos.Cantidad < Cantidad)
            {
                return false;
            }
            else
            {
                productos.Cantidad = productos.Cantidad - Cantidad;
                _context.SaveChanges();
                return true;
            }

                
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}