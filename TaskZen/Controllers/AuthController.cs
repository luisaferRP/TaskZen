using Microsoft.AspNetCore.Mvc;
using TaskZen.DTOs;
using TaskZen.Interfaces.IUser;

namespace TaskZen.Controllers
{
    public class AuthController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(Register);
            }
            await _userService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(Index);
            }
            var user = await _userService.GetUserByEmail(model.Email);
            if (user == null)
            {
                return View(Index);
            }
            if (!_userService.VerifyPassword(user.Password, model.Password))
            {
                return View(Index);
            }
            return RedirectToAction("Index", "Tasks");
        }
    }
}
