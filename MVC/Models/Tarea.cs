using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public partial class Tarea
    {
        public int Id { get; set; }
        public string? IdPadre { get; set; }
        public string? Responsables { get; set; }
        public string? Observacion { get; set; }
        public string? Descripcion { get; set; }
        public string? Empresa { get; set; }
        public string? Recursos { get; set; }
        public DateTime? FechaDeInicio { get; set; }
        public DateTime? FechaLimite { get; set; }
        public DateTime? FechaDeFinalizacion { get; set; }
        public string? Estado { get; set; }
    }
}
