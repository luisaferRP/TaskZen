using TaskZen.DTOs;
using TaskZen.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskZen.Interfaces.IUser
{
    public interface IUserService
    {
        Task Create(UserDto model);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        bool VerifyPassword(string hashedPassword, string password);
    }
}
