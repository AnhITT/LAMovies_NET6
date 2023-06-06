using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IPricingRepository _pricingRepository;


        public UserController(IUserAuthRepository userAuthRepository, IPricingRepository pricingRepository)
        {
            _userAuthRepository = userAuthRepository;
            _pricingRepository = pricingRepository;
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
            if (result.statusCode == 2)
            {
                return RedirectToAction("Index", "Home");
            }
            else if(result.statusCode == 1)
            {
                return Redirect("/Home/Dashboard");
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
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ErrorLogin()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DisplayInfoUser(int id)
        {
            var user = await _userAuthRepository.GetInfoAccount();
            ViewBag.InfoUser = user;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditInfoUser(int id)
        {
            var user = await _userAuthRepository.GetInfoAccount();
            ViewBag.InfoUser = user;
            return View();
        }
        public async Task<IActionResult> InfoServiceUser()
        {
            var pricing = await _pricingRepository.GetPricingByUser();
            var pricingEx = await _pricingRepository.CheckPricingExpired();
            ViewBag.CheckPricingExpired = pricingEx;
            return View(pricing);
        }
        [Authorize(Roles = "admin")]
        public IActionResult QLAccount()
        {
            var account = _userAuthRepository.GetAllAccount();
            return View(account);
        }
    }
}
