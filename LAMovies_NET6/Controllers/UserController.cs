using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAuthRepository _userAuthRepository;

        public UserController(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _userAuthRepository.LoginAsync(model);
            if (result.statusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.message;
                return RedirectToAction(nameof(Login));
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationDTO model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this._userAuthRepository.RegisterAsync(model);
            if (result.statusCode == 1)
            {
                LoginDTO loginDTO = new LoginDTO() { userName = model.userName, password = model.password };
                await Login(loginDTO);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.message;
                return RedirectToAction(nameof(SignUp));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._userAuthRepository.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
