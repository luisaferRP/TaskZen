using Microsoft.AspNetCore.Mvc;
using TaskZen.Models.Task;

namespace TaskZen.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {

        List<TaskModel> tasks = new List<TaskModel>
        {
            new TaskModel { Id = 1, Title = "Configurar Base de Datos", Description = "Definir estructura inicial", Priority = PriorityLevel.Alta, Status = StatusLevel.ToDo, CreatedDate = DateTime.Now },
            new TaskModel { Id = 2, Title = "Diseñar UI", Description = "Maquetar pantalla principal", Priority = PriorityLevel.Media, Status = StatusLevel.InProgress, CreatedDate = DateTime.Now },
            new TaskModel { Id = 3, Title = "Desplegar en Servidor", Description = "Publicar aplicación", Priority = PriorityLevel.Baja, Status = StatusLevel.Done, CreatedDate = DateTime.Now }

        };
            return View(tasks);
        }
    }
}
