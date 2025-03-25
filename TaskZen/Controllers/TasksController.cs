using Microsoft.AspNetCore.Mvc;
using TaskZen.Models;
using TaskZen.Data;
using Microsoft.EntityFrameworkCore;
using TaskZen.Repositories;
using TaskZen.Interfaces.ITasks;
using System.Security.Claims;

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

            var tasks = await _taskRepository.GetTasks(label, userId);


            // Pasar el nombre del usuario a la vista
            ViewBag.UserName = User.Identity.Name;

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

            task.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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
    }
}
