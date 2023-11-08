using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult Get()
        {
            try
            { 
                var  cuartosFrios = _context.CuartosFrios.ToList();

                if (cuartosFrios.Count == 0)
                {
                    return NotFound("No hay Cuartos");
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

                return BadRequest(ex.Message);
            }
        }

        // METODO PARA LEER LOS CUARTOS SEGUN SU ID

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(id);

                if (cuartosFrios == null)
                {
                    return NotFound($"No hay cuartos con codigo: {id}");
                }
                return Ok(cuartosFrios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA CREAR CuartosFrios

        [HttpPost]
        public IActionResult Post(CuartosFrios model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Cuarto Agregado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA ACTUALIZAR Cuartos

        [HttpPut]

        public IActionResult Put(CuartosFrios model)
        {
            if (model == null || model.IdCuarto == 0)
            {
                if (model == null)
                {
                    return BadRequest("El modelo de datos no es valido");
                }
                else if (model.IdCuarto == 0)
                {
                    return BadRequest($"El codigo de cuarto {model.IdCuarto} no es valido");
                }
            }

            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(model.IdCuarto);

                if (cuartosFrios == null)
                {
                    return BadRequest($"El codigo de cuarto {model.IdCuarto} no es valido");
                }

                cuartosFrios.CapacidadMaxima = model.CapacidadMaxima;
                cuartosFrios.CantidadActual = model.CantidadActual;

                _context.SaveChanges();
                return Ok("Los detalles del Cuarto se han actualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA ELIMINAR UN EMPLEADO

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(id);

                if (cuartosFrios == null)
                {
                    return NotFound($"No existe cuarto con codigo {id}");
                }

                _context.CuartosFrios.Remove(cuartosFrios);
                _context.SaveChanges();

                return Ok("Registro eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

}