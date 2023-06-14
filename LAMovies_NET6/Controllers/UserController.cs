using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
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
        public async Task<IActionResult> DisplayInfoUser()
        {
            var user = await _userAuthRepository.GetInfoAccount();
            return View(user);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userAuthRepository.GetInfoAccount();
            var checkPasswordDTO = new CheckPasswordDTO()
            {
                 id = user.Id,
                 name = user.fullName,
            };
            return View(checkPasswordDTO);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(CheckPasswordDTO checkPasswordDTO)
        {
            if (!ModelState.IsValid) {
                return View(checkPasswordDTO); 
            }
            var result = await _userAuthRepository.UpdatePassword(checkPasswordDTO);
            if (result.statusCode == 1)
            {
                TempData["true"] = result.message;
                return RedirectToAction(nameof(ChangePassword));
            }
            else
            {
                TempData["msg"] = result.message;
                return RedirectToAction(nameof(ChangePassword));
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditInfoUser()
        {
            var user = await _userAuthRepository.GetInfoAccount();
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditInfoUser(User user)
        {
            var result = await _userAuthRepository.Update(user);
            if (result)
            {
                return RedirectToAction(nameof(DisplayInfoUser));
            }
            else
            {
                TempData["msg"] = "Edit Error";
                return View(user);
            }
        }

        [Authorize]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAccount(String id)
        {
            var result = await _userAuthRepository.DeleteAccount(id);
            return RedirectToAction(nameof(QLAccount));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditAccount(String id)
        {
            var user = await _userAuthRepository.GetAccountById(id);
            return View(user);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(User user)
        {
            var result = await _userAuthRepository.Update(user);
            if (result)
            {
                return RedirectToAction(nameof(QLAccount));
            }
            else
            {
                TempData["msg"] = "Edit Error";
                return View(user);
            }
        }
    }
}
