using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

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
        public async Task<List<Tarea>> Get()
            => await _context.Tareas.ToListAsync();
        //Crear
        [HttpPost]
        public async Task<IActionResult> Create(Tarea model)
        {
            if (ModelState.IsValid)
            {
                var tarea = new Tarea();
                {
                    tarea.Responsables = model.Responsables,
                    tarea.Descripcion = model.Descripcion;
                }
            }
            return;
        }
        //Modificacion
        [HttpPut]
        public async Task<List<Tarea>> Put() => await _context.Tareas.ToListAsync();
        //Delete
        [HttpDelete]
        public async Task<List<Tarea>> Delete() => await _context.Tareas.ToListAsync()



    }
}
