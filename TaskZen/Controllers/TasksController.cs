using Microsoft.AspNetCore.Mvc;
using TaskZen.Models.Task;
using TaskZen.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskZen.Controllers
{
    public class TasksController : Controller
    {
        public readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks);
        }


        //crear nueva tarea
        public IActionResult Nueva()
        {
            var model = new TaskModel();
            //le envio la intancia de TaskModel para poder usar los enum
            return View("FormularioTarea", model);
        }

        //editar
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            TaskModel task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View("FormularioTarea", task);
        }

        //metodo para crear o actualizar tarea
        [HttpPost]
        public async Task<IActionResult> GuardarTarea(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return View("FormularioTarea", task);
            }

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
            TaskModel task = await _context.Tasks.FindAsync(id);

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
