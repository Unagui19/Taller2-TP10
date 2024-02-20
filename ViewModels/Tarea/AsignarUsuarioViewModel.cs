using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.ViewModels
{
    public class AsignarUsuarioViewModel
    {
        
        public int IdTarea{get;set;}
        public int IdTablero{get;set;}
        [Required][StringLength(30)]public string NombreTarea{get;set;}
        public int IdUsuarioAsignado{get;set;}
        public List<int> IdsDisponibles{get;set;}

        public AsignarUsuarioViewModel(){
        }

        public AsignarUsuarioViewModel(Tarea tarea, List<int> IdsDisponibles){
            IdTarea = tarea.Id;
            IdTablero = tarea.IdTablero;
            NombreTarea = tarea.Nombre;
            this.IdsDisponibles = IdsDisponibles;
        }

        // public List<int?> ObtenerIds(){
        //     var repositorio = new UsuarioRepository();
        //     List<int>ids = repositorio.ListarUsuarios().Select(usu => usu.Id ).ToList();
        // }
    }
}