using TaskZen.Models;

namespace TaskZen.Interfaces.ITasks
{
    public interface ITasksRepository
    {
        Task<List<TaskModel>> GetTasks(string? label);
        Task<TaskModel?> GetById(int id);
        Task Update(TaskModel task);
        Task Delete(int id);
        Task Add(TaskModel task);
    }
}
