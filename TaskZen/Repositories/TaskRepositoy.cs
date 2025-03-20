using Microsoft.EntityFrameworkCore;
using TaskZen.Data;
using TaskZen.Interfaces.ITasks;
using TaskZen.Models;

namespace TaskZen.Repositories
{
    public class TaskRepositoy : ITasksRepository
    {
        public readonly AppDbContext _context;

        public TaskRepositoy(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(TaskModel task)
        {
            if (task != null) 
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var taskfind = await _context.Tasks.FindAsync(id);
            if (taskfind != null)
            {
                _context.Tasks.Remove(taskfind);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TaskModel?> GetById(int id)
        {
            var taskfind = await _context.Tasks.FindAsync(id);
            return taskfind != null ? taskfind : null;
        }

        public async Task<List<TaskModel>> GetTasks(string? label)
        {
            return string.IsNullOrEmpty(label)
                ? await _context.Tasks.ToListAsync()
               : await _context.Tasks.Where(t => t.Label.ToString() == label).ToListAsync();
        }

        public async Task Update(TaskModel task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
