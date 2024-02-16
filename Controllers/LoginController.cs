using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Controllers;

public class LoginController : Controller
{
     
    private readonly ILogger<LoginController> _logger;
    private UsuarioRepository repoUsuario;

    public LoginController(ILogger<LoginController> logger)
    {
        repoUsuario = new UsuarioRepository();
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Logueo(LoginViewModel loginUsuario){
        //Existe el usuario?
        var usuarioLogueado = repoUsuario.ListarUsuarios().FirstOrDefault(usu=> usu.NombreDeUsuario == loginUsuario.Nombre);

        if (usuarioLogueado != null)
        {
            return RedirectToAction("Index");
        }
        else//Si el usuario no coincide, es decir no esta logueado, devuelvo directamente al index
        {
            return RedirectToAction("Index");        
        }
    }

}
