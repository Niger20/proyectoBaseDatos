using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers;

[Route("api/[controller]")]
[ApiController]


    public class EntradasController : Controller
    {
        private readonly MyDBcontext _context;

            public EntradasController(MyDBcontext context)
            {
                _context = context; 
            }


            //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
            [HttpGet("Obtener")]
            public IActionResult Get()
            {
                try
                {
                    var entradas = _context.Entradas.ToList();

                    if (entradas.Count == 0) return NotFound("No hay entradas Registradas");

                    return Ok(entradas);
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
                    var entradas = _context.Entradas.Find(id);

                    if (entradas == null) return NotFound($"No hay entradas con codigo: {id}");
                    return Ok(entradas);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            //METODO PARA CREAR Empleados

            [HttpPost("Crear")]
            public IActionResult Post(Entradas model)
            {
                try
                {
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok("entrada registrada Correctamente");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            //METODO PARA ACTUALIZAR Empleados

            [HttpPut("EditarEntradas")]
            public IActionResult Put(Entradas model)
            {
                if (model == null || model.IdEntrada == 0)
                {
                    if (model == null)
                        return BadRequest("El modelo de datos no es valido");
                    if (model.IdEntrada == 0) return BadRequest($"El codigo de entrada {model.IdEntrada} no es valido");
                }

                try
                {
                    var entradas = _context.Entradas.Find(model.IdEntrada);

                    if (entradas == null) return BadRequest($"El codigo de entrada {model.IdEntrada} no es valido");

                    entradas.IdProducto = model.IdProducto;
                    entradas.IdCuarto = model.IdCuarto;
                    entradas.IdEmpleado = model.IdEmpleado;
                    entradas.Fecha = model.Fecha;
                    entradas.Cantidad = model.Cantidad;
                    entradas.Tipo = model.Tipo;


                    _context.SaveChanges();
                    return Ok("Los detalles de la entrada se han actualizado");
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
                    var entradas = _context.Entradas.Find(id);

                    if (entradas == null) return NotFound($"No existe entrada con codigo {id}");

                    _context.Entradas.Remove(entradas);
                    _context.SaveChanges();

                    return Ok("Registro eliminado correctamente");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        
    }
