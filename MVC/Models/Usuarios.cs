using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }
}
