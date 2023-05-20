using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
