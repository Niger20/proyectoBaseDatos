using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {

        private readonly MyDBcontext _context;

        public EmpleadosController(MyDBcontext context)
        {
            _context = context;
        }


        //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
        [HttpGet("Obtener")]
        public IActionResult Get()
        {
            try
            {
                var empleados = _context.Empleados.ToList();

                if (empleados.Count == 0)
                {
                    return NotFound("No hay Empleados");
                }

                return Ok(empleados);
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
                var empleados = _context.Empleados.Find(id);

                if (empleados == null)
                {
                    return NotFound($"No hay Empleados con codigo: {id}");
                }
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA CREAR Empleados

        [HttpPost("Crear")]
        public IActionResult Post(Empleados model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Empleado Agregado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA ACTUALIZAR Empleados

        [HttpPut("Editar")]

        public IActionResult Put(Empleados model)
        {
            if (model == null || model.IdEmpleado == 0)
            {
                if (model == null)
                {
                    return BadRequest("El modelo de datos no es valido");
                }
                else if (model.IdEmpleado == 0)
                {
                    return BadRequest($"El codigo de empleado {model.IdEmpleado} no es valido");
                }
            }

            try
            {
                var empleados = _context.Empleados.Find(model.IdEmpleado);

                if (empleados == null)
                {
                    return BadRequest($"El codigo de empleado {model.IdEmpleado} no es valido");
                }

                empleados.Identidad = model.Identidad;
                empleados.PrimerNombre = model.PrimerNombre;
                empleados.SegundoNombre = model.SegundoNombre;
                empleados.PrimerApellido = model.PrimerApellido;
                empleados.SegundoApellido = model.SegundoApellido;


                _context.SaveChanges();
                return Ok("Los detalles del empleado se han actualizado");
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
                var empleados = _context.Empleados.Find(id);

                if (empleados == null)
                {
                    return NotFound($"No existe empleado con codigo {id}");
                }

                _context.Empleados.Remove(empleados);
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
