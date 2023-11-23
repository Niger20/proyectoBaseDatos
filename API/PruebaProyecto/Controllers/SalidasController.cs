using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers;

[Route("api/[controller]")]
[ApiController]


    public class SalidasController : Controller
    {
        private readonly MyDBcontext _context;

        public SalidasController(MyDBcontext context)
        {
            _context = context;
        }


        //  METODO PARA LEER TODOS LOS EMPLEADOS DE LA BASE DE DATOS
        [HttpGet("Obtener")]
        public IActionResult Get()
        {
            try
            {
                var salidas = _context.Salidas.ToList();

                if (salidas.Count == 0) return NotFound("No hay salidas Registradas");
                
                var response = new 
                {
                    Code = 200,
                    Message = "Lista Salidas",
                    Data = salidas
                };

                return Ok(response);
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
                var salidas = _context.Salidas.Find(id);

                if (salidas == null) return NotFound($"No hay Salidas con codigo: {id}");
                return Ok(salidas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //METODO PARA CREAR Empleados

        [HttpPost("Crear")]
        public IActionResult Post(Salidas model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok(new { message = "salida registrada Correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //METODO PARA ACTUALIZAR Empleados

        [HttpPut("Editar")]
        public IActionResult Put(Salidas model)
        {
            if (model == null || model.IdSalida == 0)
            {
                if (model == null)
                    return BadRequest("El modelo de datos no es valido");
                if (model.IdSalida == 0) return BadRequest($"El codigo de salida {model.IdSalida} no es valido");
            }

            try
            {
                var salidas = _context.Salidas.Find(model.IdSalida);

                if (salidas == null) return BadRequest($"El codigo de salida {model.IdSalida} no es valido");

                salidas.IdProducto = model.IdProducto;
                salidas.IdCuarto = model.IdCuarto;
                salidas.IdEmpleado = model.IdEmpleado;
                salidas.Fecha = model.Fecha;
                salidas.Cantidad = model.Cantidad;
                salidas.Tipo = model.Tipo;


                _context.SaveChanges();
                return Ok("Los detalles de la salida se han actualizado");
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
                var salidas = _context.Salidas.Find(id);

                if (salidas == null) return NotFound($"No existe salida con codigo {id}");

                _context.Salidas.Remove(salidas);
                _context.SaveChanges();

                return Ok("Registro eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
