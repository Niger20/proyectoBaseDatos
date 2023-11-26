using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

namespace PruebaProyecto.Controllers;

[Route("api/[controller]")]
[ApiController]


    public class EntradasController : Controller
    {
        private static MyDBcontext _context;
        

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

                    if (entradas.Count == 0)
                    {
                        return NotFound(new { message = "No hay entradas Registradas" });
                    }

                    var response = new 
                    {
                        Code = 200,
                        Message = "Lista Salidas",
                        Data = entradas
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
                    var entradas = _context.Entradas.Find(id);

                    if (entradas == null)
                    {
                        return NotFound(new { message = $"No hay entradas con codigo: {id}" });
                    }

                    return Ok(entradas);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

//METODO PARA CREAR SALIDAS
            [HttpPost("Crear")]
            public IActionResult Post(Entradas model)
            {
                try
                {
                    if (this.ActualizarCantidadEntrada(model.IdCuarto, model.Cantidad))
                    {
                        _context.Add(model);
                        _context.SaveChanges();
                        return Ok(new { message = "entrada registrada Correctamente" });
                    }
                    else
                    {
                        return BadRequest(new { message = $"error detectado en los datos" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

//METODO PARA ACTUALIZAR Empleados
            [HttpPut("EditarEntradas")]
            public IActionResult Put(Entradas model)
            {
                if (model == null || model.IdEntrada == 0)
                {
                    if (model == null)
                    {
                        return BadRequest(new { message = "El modelo de datos no es valido" });
                    }
                    else if (model.IdEntrada == 0)
                    {
                        return BadRequest(new { message = $"El codigo de entrada {model.IdEntrada} no es valido" });
                    }
                }

                try
                {
                    var entradas = _context.Entradas.Find(model.IdEntrada);

                    if (entradas == null)
                    {
                        return BadRequest(new { message = $"El codigo de entrada {model.IdEntrada} no es valido" });
                    }

                    entradas.IdProducto = model.IdProducto;
                    entradas.IdCuarto = model.IdCuarto;
                    entradas.IdEmpleado = model.IdEmpleado;
                    entradas.Fecha = model.Fecha;
                    entradas.Cantidad = model.Cantidad;
                    entradas.Tipo = model.Tipo;

                    _context.SaveChanges();
                    return Ok(new { message = "Los detalles de la entrada se han actualizado" });
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
                    var entradas = _context.Entradas.Find(id);

                    if (entradas == null)
                    {
                        return NotFound(new { message = $"No existe entrada con codigo {id}" });
                    }

                    _context.Entradas.Remove(entradas);
                    _context.SaveChanges();

                    return Ok(new { message = "Registro eliminado correctamente" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            
            //METODO PARA ACTUALIZAR CANTIDAD DE CUARTOS Entrada
            [HttpPut("ActualizarCantidadEntrada")]
            public Boolean ActualizarCantidadEntrada(int IdCuarto, int Cantidad)
            {
                try
                {
                    var cuartosFrios = _context.CuartosFrios.Find(IdCuarto);

                    if (cuartosFrios == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (cuartosFrios.CapacidadDisponible < Cantidad)
                        {
                            return false;
                        }
                        else
                        {
                            cuartosFrios.CantidadActual += Cantidad;

                            _context.SaveChanges();
                            return true;
                        }
                       
                    
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        
    }
