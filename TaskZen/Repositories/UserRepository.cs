using Microsoft.EntityFrameworkCore;
using TaskZen.Data;
using TaskZen.Interfaces.IUser;
using TaskZen.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskZen.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {

        private readonly AppDbContext _context = context;

        public async Task Create(User model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
