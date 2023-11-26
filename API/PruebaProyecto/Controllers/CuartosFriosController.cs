using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    
public class CuartosFriosController : Controller
{
    
        private readonly MyDBcontext _context;

        public CuartosFriosController(MyDBcontext context)
        {
            _context = context;
        }


        //  METODO PARA LEER TODOS LOS CUARTOS DE LA BASE DE DATOS
        [HttpGet("Obtener")]
        public IActionResult Get()
        {
            try
            { 
                var  cuartosFrios = _context.CuartosFrios.ToList();

                if (cuartosFrios.Count == 0)
                {
                    return NotFound(new { message = "No hay cuartos registrados" });
                }


                var response = new 
                {
                    Code = 200,
                    Message = "Lista Cuartos",
                    Data = cuartosFrios
                };
                
                
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        // METODO PARA LEER LOS CUARTOS SEGUN SU ID

        [HttpGet("Buscar")]
        public IActionResult Get(int id)
        {
            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(id);

                if (cuartosFrios == null)
                {
                    return NotFound(new { message = "codigo de cuarto invalido" });
                }
                return Ok(cuartosFrios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //METODO PARA CREAR CuartosFrios

        [HttpPost("Crear")]
        public IActionResult Post(CuartosFrios model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok(new { message = "cuarto agregado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //METODO PARA ACTUALIZAR CUARTOS

        [HttpPut("Editar")]

        public IActionResult Put(CuartosFrios model)
        {
            if (model == null || model.IdCuarto == 0)
            {
                if (model == null)
                {
                    return BadRequest(new { message = "datos invalidos" });
                }
                else if (model.IdCuarto == 0)
                {
                    return BadRequest(new { message = "codigo de cuarto invalido" });
                }
            }

            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(model.IdCuarto);

                if (cuartosFrios == null)
                {
                    return BadRequest(new { message = "codigo de cuarto invalido" });
                }

                cuartosFrios.CapacidadMaxima = model.CapacidadMaxima;
                cuartosFrios.CantidadActual = model.CantidadActual;

                _context.SaveChanges();
                return Ok(new { message = "cuarto actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        

        //METODO PARA ELIMINAR UN CUARTO

        [HttpDelete("Eliminar")]

        public IActionResult Delete(int id)
        {
            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(id);

                if (cuartosFrios == null)
                {
                    return NotFound(new { message = "Codigo de cuarto invalido" });
                }

                _context.CuartosFrios.Remove(cuartosFrios);
                _context.SaveChanges();

                return Ok(new { message = "Cuarto eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });          
            }

        }

}