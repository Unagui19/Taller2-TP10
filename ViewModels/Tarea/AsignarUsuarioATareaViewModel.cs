using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.ViewModels
{
    public class AsignarUsuarioATareaViewModel
    {
        
        public int IdTarea{get;set;}
        public int IdTablero{get;set;}
        
        //[Required][StringLength(30)]
        public string NombreTarea{get;set;}
        public string NombreUsuario{get;set;}
        public int IdUsuarioAsignado{get;set ;}
        public List<Usuario> UsuariosDisponibles{get;set;}
        
        public AsignarUsuarioATareaViewModel(){}
        public AsignarUsuarioATareaViewModel(Tarea tarea, List<Usuario> UsuariosDisponibles){
            IdTarea = tarea.Id;
            IdTablero = tarea.IdTablero;
            NombreTarea = tarea.Nombre;
            this.UsuariosDisponibles = UsuariosDisponibles;
        }

        public AsignarUsuarioATareaViewModel(int idTarea, List<Usuario> UsuariosDisponibles){
            IdTarea = idTarea;
            this.UsuariosDisponibles = UsuariosDisponibles;
        }

    }
}