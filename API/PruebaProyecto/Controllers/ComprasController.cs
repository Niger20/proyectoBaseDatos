namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class ComprasController : ControllerBase
{
    private static MyDBcontext _context;
    

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

            if (compras.Count == 0) return NotFound(new { message = "No hay compras registradas" });

            var response = new
            {
                Code = 200,
                Message = "Lista Ventas",
                Data = compras
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
            var compras = _context.Compras.Find(id);

            if (compras == null) return NotFound(new { message = "Codigo de compra no valido" });
            return Ok(compras);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    //METODO PARA CREAR Empleados

    [HttpPost("Crear")]
    public IActionResult Post(Compras model)
    {
        try
        {
            if (this.ActualizarCantidadCompra(model.IdProducto, model.Cantidad))
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok(new { message = "Compra agregada correctamente" });   
            }
            else
            {
                return BadRequest(new { message = "error en los datos, verifiquelos" });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    //METODO PARA ACTUALIZAR Empleados

    [HttpPut("Editar")]
    public IActionResult Put(Compras model)
    {
        if (model == null || model.IdCompra == 0)
        {
            if (model == null)
                return BadRequest(new { message = "datos no validos" });
            if (model.IdCompra == 0) return BadRequest(new { message = "Codigo de compra invalido" });
        }

        try
        {
            var compras = _context.Compras.Find(model.IdCompra);

            if (compras == null) return BadRequest(new { message = "Codigo de compra invalido" });

            compras.IdProveedor = model.IdProveedor;
            compras.IdProducto = model.IdProducto;
            compras.Precio = model.Precio;
            compras.Fecha = model.Fecha;
            compras.Cantidad = model.Cantidad;


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
            var compras = _context.Compras.Find(id);

            if (compras == null) return NotFound(new { message = "Codigo de compra invalido" });

            _context.Compras.Remove(compras);
            _context.SaveChanges();

            return Ok(new { message = "registro eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPut("ActualizarCantidadCompra")]
        
    //METODO PARA ACTUALIZAR CANTIDAD DE productos compra
    public Boolean ActualizarCantidadCompra(int IdProducto, int Cantidad)
    {
        try
        {
            var productos = _context.Productos.Find(IdProducto);

            if (productos == null)
            {
                return false;
            }
                
            productos.Cantidad = productos.Cantidad + Cantidad;

            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}