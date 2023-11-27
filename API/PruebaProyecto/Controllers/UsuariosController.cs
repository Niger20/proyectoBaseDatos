namespace PruebaProyecto.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaProyecto.DAL;
using PruebaProyecto.Models;

[Route("api/[controller]")]
[ApiController]

public class UsuariosController : ControllerBase
{
    private readonly MyDBcontext _context;

    public UsuariosController(MyDBcontext context)
    {
        _context = context;
    }
    
    //METODO PARA CREAR Usuarios

    [HttpPost( "Crear")]
    public IActionResult Post(Usuarios model)
    {
        Encrypt encrypt = new Encrypt();
        try
        {
            Usuarios modelo = new Usuarios();
            modelo.Username = model.Username;
            modelo.Password = encrypt.GetSHA256(model.Password);
            modelo.Rol = model.Rol;
            
            _context.Add(modelo);
            _context.SaveChanges();
            return Ok(new {message = "Usuario agregado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message });
        }
    }
    
    
    //METODO PARA ELIMINAR UN usuario

    [HttpDelete("Eliminar")]

    public IActionResult Delete(string username)
    {
        try
        {
            var usuarios = _context.Usuarios.Find(username);

            if (usuarios == null)
            {
                return NotFound(new { message = "nombre de usuario no valido" });
            }

            _context.Usuarios.Remove(usuarios);
            _context.SaveChanges();

            return Ok(new { message = "Cuarto eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });          
        }

    }
    
    //  METODO PARA LEER TODOS LOS usuarios DE LA BASE DE DATOS
    [HttpGet("Obtener")]
    public IActionResult Get()
    {
        try
        { 
            var  usuarios = _context.Usuarios.ToList();

            if (usuarios.Count == 0)
            {
                return NotFound(new { message = "No hay usuarios registrados" });
            }


            var response = new 
            {
                Code = 200,
                Message = "Lista Usuarios",
                Data = usuarios
            };
                
                
            return Ok(response);
        }
        catch (Exception ex)
        {

            return BadRequest(new { message = ex.Message });
        }
    }
    
    //METODO PARA LOGGEAR Usuarios

    [HttpPost( "Login")]
    public IActionResult PostLogin(Usuarios model)
    {
        Encrypt encrypt = new Encrypt();
        
        try
        {
            var usuarios = _context.Usuarios.Find(model.Username);

            if (usuarios == null)
            {
                return NotFound(new {message = "USUARIO O CONTRASENA INCORRECTAS" });
            }
            else
            {
                if (encrypt.GetSHA256(model.Password) == usuarios.Password)
                {
                    var response = new
                    {
                        Username = usuarios.Username,
                        LoginStatus = "valido",
                        Rol = usuarios.Rol
                    };

                    return Ok(response);
                }
                else
                {
                    return BadRequest(new {message = "Usuario o contrasena incorrecta" });
                }   
            } 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}