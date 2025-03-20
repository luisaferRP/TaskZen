using Microsoft.AspNetCore.Mvc;
using TaskZen.Models;
using TaskZen.Data;
using Microsoft.EntityFrameworkCore;
using TaskZen.Repositories;
using TaskZen.Interfaces.ITasks;

namespace TaskZen.Controllers
{
    public class TasksController : Controller
    {

        private readonly ITasksRepository _taskRepository;

        public TasksController(ITasksRepository taskRepository)
        {
            _taskRepository = taskRepository;

        }

        public async Task<IActionResult> Index(string? label = null)
        {
            var tasks = await _taskRepository.GetTasks(label);
            return View(tasks);
        }

        //crear
        public IActionResult Nueva()
        {
            return View("FormularioTarea", new TaskModel());
        }


        //actualizar
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View("FormularioTarea", task);
        }


        [HttpPost]
        public async Task<IActionResult> GuardarTarea(TaskModel task)
        {
            if (!ModelState.IsValid) return View("FormularioTarea", task);

            if (task.Id > 0)
            {
                await _taskRepository.Update(task);
            }
            else
            {
                task.CreatedDate = DateTime.Now;
                await _taskRepository.Add(task);
            }

            return RedirectToAction(nameof(Index));
        }

        //eliminar
        public async Task<IActionResult> Eliminar(int id)
        {
            await _taskRepository.Delete(id);
            return RedirectToAction(nameof(Index));

        }




        //    public readonly AppDbContext _context;

        //    public TasksController(AppDbContext context) 
        //    {
        //        _context = context;
        //    }

        //    public async Task<IActionResult> Index(string? label = null)
        //    {
        //        var task = string.IsNullOrEmpty(label) ? await _context.Tasks.ToListAsync() :
        //            await _context.Tasks.Where(t => t.Label.ToString() == label).ToListAsync();
        //        return View(task);
        //    } 


        //    //crear nueva tarea
        //    public IActionResult Nueva()
        //    {
        //        var model = new Task();
        //        //le envio la intancia de TaskModel para poder usar los enum
        //        return View("FormularioTarea", model);
        //    }

        //    //editar
        //    [HttpGet]
        //    public async Task<IActionResult> Editar(int id)
        //    {
        //        Task task = await _context.Tasks.FindAsync(id);

        //        if (task == null)
        //        {
        //            return NotFound();
        //        }

        //        return View("FormularioTarea", task);
        //    }

        //    //metodo para crear o actualizar tarea
        //    [HttpPost]
        //    public async Task<IActionResult> GuardarTarea(Task task)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View("FormularioTarea", task);
        //        }

        //        if (task.Id > 0) 
        //        {
        //            _context.Tasks.Update(task);
        //        }
        //        else 
        //        {
        //            task.CreatedDate = DateTime.Now;
        //            await _context.Tasks.AddAsync(task);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }



        //    [HttpDelete]
        //    public async Task<IActionResult> Eliminar(int id)
        //    {
        //        Task task = await _context.Tasks.FindAsync(id);

        //        if (task == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Tasks.Remove(task);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
        //}
    }
}
