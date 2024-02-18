using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Controllers;

public class LoginController : Controller
{
     
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _repoUsuario;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository repoUsuario)
    {
        _repoUsuario = repoUsuario;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Logueo(LoginViewModel loginUsuario){
        Usuario usuarioLogueado = new Usuario();
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        try
        {
            usuarioLogueado = usuarios.FirstOrDefault(usu => usu.NombreDeUsuario == loginUsuario.Nombre && usu.Contrasenia == loginUsuario.Contrasenia);
             if (usuarioLogueado == null)
            {
                var loginVM = new LoginViewModel() ;
                // {
                //     MensajeDeError = "Usuario no existente"
                // };
                _logger.LogWarning("Intento de acceso invalido - Usuario:" + loginUsuario.Nombre + "Clava ingresada: " + loginUsuario.Contrasenia);
                return View("Index",loginVM); 
            }
            
            _logger.LogInformation("El usuario" + usuarioLogueado.NombreDeUsuario + "ingreso correctamente");
            HttpContext.Session.SetInt32("IdUsuario", usuarioLogueado.Id);
            HttpContext.Session.SetString("Usuario", usuarioLogueado.NombreDeUsuario);
            // HttpContext.Session.SetString("Contraseña", user.Contrasenia);
            HttpContext.Session.SetString("Rol", usuarioLogueado.Rol.ToString());
            return RedirectToAction("Index","Home");
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            return BadRequest(RedirectToAction("Index"));

        }

    }
}

//Existe el usuario?
    // private void LoguearUsuario(Usuario user)
    // {
    //     HttpContext.Session.SetInt32("IdUsuario", user.Id);
    //     HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
    //     // HttpContext.Session.SetString("Contraseña", user.Contrasenia);
    //     HttpContext.Session.SetString("Rol", user.Rol.ToString());
    // }
    