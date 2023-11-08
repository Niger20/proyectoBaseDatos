using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly MyDBcontext _context;

        public ProductosController(MyDBcontext context)
        {
            _context = context;
        }


        //  METODO PARA LEER TODOS LOS PRODUCTOS DE LA BASE DE DATOS
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var productos = _context.Productos.ToList();

                if (productos.Count == 0)
                {
                    return NotFound("No hay Productos");
                }

                return Ok(productos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // METODO PARA LEER LOS PRODUCTOS SEGUN SU ID

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var productos = _context.Productos.Find(id);

                if (productos == null)
                {
                    return NotFound($"No hay productos con codigo: {id}");
                }
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA CREAR PRODUCTOS

        [HttpPost]
        public IActionResult Post(Productos model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Producto Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA ACTUALIZAR PRODUCTOS

        [HttpPut]

        public IActionResult Put(Productos model)
        {
            if (model == null || model.IdProducto == 0)
            {
                if (model == null)
                {
                    return BadRequest("El modelo de datos no es valido");
                }
                else if (model.IdProducto == 0)
                {
                    return BadRequest($"El codigo de producto {model.IdProducto} no es valido");
                }
            }

            try
            {
                var productos = _context.Productos.Find(model.IdProducto);

                if (productos == null)
                {
                    return BadRequest($"El codigo de producto {model.IdProducto} no es valido");
                }

                productos.Descripcion = model.Descripcion;
                productos.Cantidad = model.Cantidad;
                productos.Peso = model.Peso;

                _context.SaveChanges();
                return Ok("Los detalles del producto se han actualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA ELIMINAR UN PRODUCTO

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            try
            {
                var productos = _context.Productos.Find(id);

                if (productos == null)
                {
                    return NotFound($"No existe producto con codigo {id}");
                }

                _context.Productos.Remove(productos);
                _context.SaveChanges();

                return Ok("Registro eliminado correctamente");
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }
    }
}
