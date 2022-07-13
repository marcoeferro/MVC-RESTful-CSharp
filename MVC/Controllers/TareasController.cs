using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Views.Tareas.ViewModels;

namespace MVC.Controllers
{
    public class TareasController : Controller
    {
        private readonly TareasContext _context;

        public TareasController(TareasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tareas.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Tareas"] = new SelectList(_context.Tareas, "Responsables","Responsables");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TareaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tarea = new Tarea()
                {
                    Responsables = model.Responsable,
                    Descripcion = model.Descripcion
                };
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tareas"] = new SelectList(_context.Tareas, "Responsables","Responsables");
            return View();
        }
    }
}
