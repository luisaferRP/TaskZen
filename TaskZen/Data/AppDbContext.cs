using Microsoft.EntityFrameworkCore;

namespace TaskZen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
    
}
