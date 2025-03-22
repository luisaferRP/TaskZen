using Microsoft.AspNetCore.Mvc;
using TaskZen.Models;
using TaskZen.Data;
using Microsoft.EntityFrameworkCore;
using Task = TaskZen.Models.Task;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using System.Security.Claims;

namespace TaskZen.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class TasksController : Controller
    {
        public readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? label = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Auth");
            }

            // Obtener el userId desde los claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "Auth"); // Si no tiene el claim, redirigir a login
            }

            int userId = int.Parse(userIdClaim); // Convertir el userId a entero

            // Obtener las tareas filtradas por usuario
            var tasks = string.IsNullOrEmpty(label) ?
                await _context.Tasks.Where(t => t.UserId == userId).ToListAsync() :
                await _context.Tasks.Where(t => t.UserId == userId && t.Label.ToString() == label).ToListAsync();

            // Pasar el nombre del usuario a la vista
            ViewBag.UserName = User.Identity.Name;

            return View(tasks);
        }


        //crear nueva tarea
        public IActionResult Nueva()
        {
            var model = new Task();
            //le envio la intancia de TaskModel para poder usar los enum
            return View("FormularioTarea", model);
        }

        //editar
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Task task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View("FormularioTarea", task);
        }

        //metodo para crear o actualizar tarea
        [HttpPost]
        public async Task<IActionResult> GuardarTarea(Task task)
        {
            if (!ModelState.IsValid)
            {
                return View("FormularioTarea", task);
            }

            task.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (task.Id > 0) 
            {
                _context.Tasks.Update(task);
            }
            else 
            {
                task.CreatedDate = DateTime.Now;
                await _context.Tasks.AddAsync(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            Task task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
