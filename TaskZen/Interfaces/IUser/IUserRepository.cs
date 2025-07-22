using TaskZen.DTOs;
using TaskZen.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskZen.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task Create(User model);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
    }
}
