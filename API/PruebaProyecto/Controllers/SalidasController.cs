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
        private static MyDBcontext _context;
        

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

                if (salidas.Count == 0) return NotFound(new {message = "no hay registros de salidas"});
                
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
                return BadRequest(new { message = ex.Message });
            }
        }

        // METODO PARA LEER LOS EMPLEADOS SEGUN SU ID

        [HttpGet("Buscar")]
        public IActionResult Get(int id)
        {
            try
            {
                var salidas = _context.Salidas.Find(id);

                if (salidas == null) return NotFound(new { message = $"No hay Salidas con codigo: {id}" });
                return Ok(salidas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        //METODO PARA CREAR salidas

        [HttpPost("Crear")]
        public IActionResult Post(Salidas model)
        {
            try
            {

                if (this.ActualizarCantidadSalida(model.IdCuarto,model.Cantidad))
                {
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(new { message = "salida registrada Correctamente" });
                }
                else
                {
                    return BadRequest(new { message = "Error detectado, verifique los datos ingresados" });
                }
                
                
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
                    return BadRequest(new { message = "Los datos no son validos"});
                if (model.IdSalida == 0) return BadRequest(new { message = "el codigo de salida no es valido"});
            }

            try
            {
                var salidas = _context.Salidas.Find(model.IdSalida);

                if (salidas == null) return BadRequest(new { message = "el codigo de salida no es valido"});

                salidas.IdProducto = model.IdProducto;
                salidas.IdCuarto = model.IdCuarto;
                salidas.IdEmpleado = model.IdEmpleado;
                salidas.Fecha = model.Fecha;
                salidas.Cantidad = model.Cantidad;
                salidas.Tipo = model.Tipo;


                _context.SaveChanges();
                return Ok(new { message = "Los detalles se han actualizado correctamente"});
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
                var salidas = _context.Salidas.Find(id);

                if (salidas == null) return NotFound(new { message = "No existe salida con ese codigo"});

                _context.Salidas.Remove(salidas);
                _context.SaveChanges();

                return Ok(new { message = "Registro eliminado correctamente"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        //METODO PARA ACTUALIZAR CANTIDAD DE CUARTOS Salida
        [HttpPut("ActualizarCantidadSalida")]
        public Boolean ActualizarCantidadSalida(int IdCuarto, int Cantidad)
        {
            try
            {
                var cuartosFrios = _context.CuartosFrios.Find(IdCuarto);

                if (cuartosFrios == null)
                {
                    return false;
                }

                if (cuartosFrios.CantidadActual < Cantidad)
                {
                    return false;
                }
                else
                {
                    cuartosFrios.CantidadActual = cuartosFrios.CantidadActual - Cantidad;
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
