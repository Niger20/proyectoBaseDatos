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
            return Ok("usuario Agregado Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
                return NotFound($"USUARIO O CONTRASENA INCORRECTA");
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
                    return BadRequest("Usuario o contrasena incorrectas");
                }   
            } 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        try
        {
            Usuarios modelo = new Usuarios();
            modelo.Username = model.Username;
            modelo.Password = encrypt.GetSHA256(model.Password);
            modelo.Rol = model.Rol;
            
            _context.Add(modelo);
            _context.SaveChanges();
            return Ok("usuario Agregado Correctamente");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}