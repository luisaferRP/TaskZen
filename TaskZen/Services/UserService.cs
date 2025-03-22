using AutoMapper;
using TaskZen.Data;
using TaskZen.DTOs;
using TaskZen.Interfaces.IUser;
using TaskZen.Models;
using TaskZen.Security;
using Task = System.Threading.Tasks.Task;

namespace TaskZen.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, PasswordHasherService passwordHasherService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly PasswordHasherService _passwordHasherService = passwordHasherService;

        public async Task Create(UserDto model)
        {
            var user = _mapper.Map<User>(model);

            user.Password = _passwordHasherService.HashPassword(model.Password);

            await _userRepository.Create(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return _passwordHasherService.VerifyPassword(hashedPassword, password);
        }
    }
}
