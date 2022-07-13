using System.ComponentModel.DataAnnotations;

namespace MVC.Views.Tareas.ViewModels
{
    public class TareaViewModel
    {
        [Required]
        public string Responsable { get; set; }
        
        [Required]
        public string Descripcion { get; set; }
    }
}
