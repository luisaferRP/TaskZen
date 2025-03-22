using Microsoft.AspNetCore.Mvc;
using TaskZen.Config;
using TaskZen.DTOs;
using TaskZen.Interfaces.IUser;

namespace TaskZen.Controllers
{
    public class AuthController(IUserService userService, JWTConfig jwtConfig) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly JWTConfig _jwtConfig = jwtConfig;

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
                return View("Register", model);
            }

            await _userService.Create(model);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _userService.GetUserByEmail(model.Email);
            if (user == null || !_userService.VerifyPassword(user.Password, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
                return View("Index", model);
            }

            var token = _jwtConfig.GenerateJwtToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Asegúrate de usar HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(60)
            };

            Response.Cookies.Append("AuthToken", token, cookieOptions);

            return RedirectToAction("Index", "Tasks");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Index", "Auth");
        }
    }
}
