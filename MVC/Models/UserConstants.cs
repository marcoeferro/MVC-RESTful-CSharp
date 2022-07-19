namespace MVC.Models
{
    public class UserConstants
    {
        
        public static List<Usuarios> Users = new List<Usuarios>()
        {
            new Usuarios() { Usuario = "jason_admin", Email = "jason.admin@email.com", 
                Contrasenia = "MyPass_w0rd", Nombre = "Jason", Apellido = "Bryant", Rol = "Administrator" },
            new Usuarios() { Usuario = "elyse_seller", Email = "elyse.seller@email.com", 
                Contrasenia = "MyPass_w0rd", Nombre = "Elyse", Apellido = "Lambert", Rol = "Seller" },
        };
        
    }
}
