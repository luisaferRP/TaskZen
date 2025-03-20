using Microsoft.AspNetCore.Mvc;

namespace TaskZen.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {

            List<Models.Task> tasks = new List<Models.Task>
        {
            new Models.Task { Id = 1, Title = "Configurar Base de Datos", Description = "Definir estructura inicial", Priority = PriorityLevel.Alta, Status = StatusLevel.ToDo, CreatedDate = DateTime.Now },
            new Models.Task { Id = 2, Title = "Diseñar UI", Description = "Maquetar pantalla principal", Priority = PriorityLevel.Media, Status = StatusLevel.InProgress, CreatedDate = DateTime.Now },
            new Models.Task { Id = 3, Title = "Desplegar en Servidor", Description = "Publicar aplicación", Priority = PriorityLevel.Baja, Status = StatusLevel.Done, CreatedDate = DateTime.Now }

        };
            return View(tasks);
        }
    }
}
