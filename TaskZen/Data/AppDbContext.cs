using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskZen.Models;

namespace TaskZen.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
            public DbSet<User> Users { get; set; }
            public DbSet<TaskModel> Tasks { get; set; }
    }
}
