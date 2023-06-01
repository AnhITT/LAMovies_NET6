using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LAMovies_NET6.Controllers
{
    public class PricingController : Controller
    {
        private readonly IPricingRepository _pricingRepository;
        private readonly IUserAuthRepository _userAuthRepository;

        public PricingController(IPricingRepository pricingRepository, IUserAuthRepository userAuthRepository)
        {
            _pricingRepository = pricingRepository;
            _userAuthRepository = userAuthRepository;
        }
        public IActionResult Pricing()
        {
            var pricing = _pricingRepository.GetAllPricings().ToList();
            return View(pricing);
        }
        public IActionResult PricingNone()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult QLPricing()
        {
            var pricing = _pricingRepository.GetAllPricings().ToList();
            return View(pricing);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> QLUserPricing()
        {
            var listUserPricing = _pricingRepository.GetAllUserPricings().ToList();
            List<ListUserPricingDTO> list = new List<ListUserPricingDTO> ();
            foreach(var item in listUserPricing)
            {
                var user = await _userAuthRepository.GetAccountById(item.idUser);
                var pricing = await _pricingRepository.GetPricingById(item.idPricing);
                ListUserPricingDTO userPricingDTO = new ListUserPricingDTO()
                {
                    usernameUser = user.UserName,
                    fullNameUser = user.fullName,
                    namePricing = pricing.namePricing,
                    startTime = item.startTime,
                    endTime = item.endTime,
                    remainingTime = item.endTime - DateTime.Now
                };
                list.Add(userPricingDTO);
            }
            return View(list);
        }
    }
}
