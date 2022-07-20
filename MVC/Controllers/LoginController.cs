using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVC.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _config;
        public readonly TareasContext _context;

        public LoginController(IConfiguration config, TareasContext context)
        {
            _config = config;
            _context = context;
        }        
        
        //Login
        [AllowAnonymous]
        [HttpPost]       
        public IActionResult login([FromBody] UserLogin userlogin)
        {
            var user = Authenticate(userlogin);

            if (user != null)
            {
                //crea el token con lo especificado 
                var Token = Generate(user);
                return Ok(Token);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        // Crea el Token para el Usuario        
        private object Generate(Usuarios user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Usuario),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Nombre),
                new Claim(ClaimTypes.Surname, user.Apellido),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        //si el user name y la contraseña del usuario logeado coincide con las de algun usuario devuelve el usuario logeado sino devuelve nulo 
        private Usuarios Authenticate(UserLogin userlogin)
        {
            //var currentUser = TareasContext.User.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);
            var currentUser = UserConstants.Users.FirstOrDefault(o => (o.Usuario.ToLower() == userlogin.UserName.ToLower() && o.Contrasenia == userlogin.Password));

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

    }
 
}
