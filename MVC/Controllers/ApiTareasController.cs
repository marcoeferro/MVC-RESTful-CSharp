using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ApiTareasController : ControllerBase
    {
        public readonly TareasContext _context;

        public ApiTareasController(TareasContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        public  List<Tarea> Get()
        {
            return _context.Tareas.ToList();
        }

        
        //Crear
        [HttpPost]
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
        [HttpPut]
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
        [HttpDelete]
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



    }
}
