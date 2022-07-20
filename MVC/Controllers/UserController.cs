using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        public readonly TareasContext _context;

        public UserController(TareasContext context)
        {
            _context = context;
        }


        [HttpGet("Test")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Nombre}, you are an {currentUser.Rol}");
        }
        //Get
        [HttpGet("Read")]
        [Authorize]
        public  List<Tarea> Get()
        {   
            return _context.Tareas.ToList();
        }

        
        //Crear
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]Tarea model)
        {
            if (ModelState.IsValid)
            {
                var tarea = new Tarea();
                {
                    tarea.Responsables = model.Responsables;
                    tarea.Descripcion = model.Descripcion;
                }
                _context.Add(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest();
        }
        
        //Modificacion
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Tarea model) { 
            if (ModelState.IsValid)
            {
                foreach (var tarea in _context.Tareas)
                {
                    if(tarea.Id == model.Id)
                    {
                        _context.Update(tarea);
                        await _context.SaveChangesAsync();
                        
                        return Ok(model);
                    }
                }
            }
            return NotFound(model);
        }

        //Delete
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody]Tarea model) {
            if (ModelState.IsValid)
            {
                foreach (var tarea in _context.Tareas)
                {
                    if (tarea.Id == model.Id)
                    {
                        _context.Remove(tarea);
                        await _context.SaveChangesAsync();

                        return Ok(model);
                    }
                }
            }
            return NotFound(model);
        }


        private Usuarios GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new Usuarios
                {
                    Usuario = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Nombre = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Apellido = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Rol = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }

    }
}
