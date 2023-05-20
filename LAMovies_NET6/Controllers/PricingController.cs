using LAMovies_NET6.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Controllers
{
    public class PricingController : Controller
    {
        private readonly IPricingRepository _pricingRepository;
        public PricingController(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;

        }
        public IActionResult Pricing()
        {
            var pricing = _pricingRepository.GetAllPricings().ToList();
            return View(pricing);
        }
    }
}
