using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _repoTarea;
    private readonly IUsuarioRepository _repoUsuario;

    public TareaController(ILogger<TareaController> logger, ITareaRepository repoTarea, IUsuarioRepository repoUsuario)
    {
        _repoTarea = repoTarea;
        _repoUsuario = repoUsuario;
        _logger = logger;
    }

//Listar Usuarios
    public IActionResult Index()
    {
            List<Tarea> tareas = _repoTarea.ListarTareas();
            var VModel = tareas.Select(tar => new IndexTareaViewModel(tar)).ToList();
            return View(VModel);
    }

//Crear Usuario
    [HttpGet]
    public IActionResult CrearTarea(){
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel nuevaTarea){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        var tarea = new Tarea(nuevaTarea);
        _repoTarea.CrearTarea(tarea);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea){
        var VModel = new ModificarTareaViewModel(_repoTarea.BuscarTareaPorId(idTarea));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel modTarea){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        var tarea = new Tarea(modTarea);
        _repoTarea.ModificarTarea(tarea.Id,tarea);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTarea(int idTarea){
        _repoTarea.EliminarTarea(idTarea);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult AsignarUsuarioATarea(int idTarea){
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        // var VModel = new AsignarUsuarioATareaViewModel(idTarea, usuarios);
        var VModel = new AsignarUsuarioATareaViewModel(_repoTarea.BuscarTareaPorId(idTarea), usuarios);
        return View(VModel);
    }

    [HttpPost]
    public IActionResult AsignarUsuarioATarea(AsignarUsuarioATareaViewModel asignarId){
        // if (!ModelState.IsValid){return RedirectToAction("Index");}
        var usuario = _repoUsuario.BuscarUsuarioPorId(asignarId.IdUsuarioAsignado);
        // var tarea = new Tarea(asignarId);
        _repoTarea.AsignarUsuarioATarea(usuario.Id, asignarId.IdTarea);
        return RedirectToAction("Index");
    }

    // public bool usuarioLogueado(){
    //    if (HttpContext.Session.IsAvailable)
    //    {
    //         return true;
    //    } 
    //    else
    //    {
    //         return false;
    //    }
    // }
}