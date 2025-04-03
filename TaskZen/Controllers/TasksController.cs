using Microsoft.AspNetCore.Mvc;
using TaskZen.Models;
using TaskZen.Data;
using Microsoft.EntityFrameworkCore;
using TaskZen.Repositories;
using TaskZen.Interfaces.ITasks;
using System.Security.Claims;
using TaskZen.DTOs;

namespace TaskZen.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoTarea([FromBody] UpdateTaskStatusDto model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                {
                    return Json(new { success = false, message = "Datos inválidos" });
                }

                var task = await _taskRepository.GetById(model.Id);
                if (task == null)
                {
                    return Json(new { success = false, message = "Tarea no encontrada" });
                }

                if (!Enum.IsDefined(typeof(StatusLevel), model.Status))
                {
                    return Json(new { success = false, message = "Estado inválido" });
                }

                task.Status = (StatusLevel)model.Status;
                await _taskRepository.Update(task);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ActualizarEstadoTarea: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }
    }
}
