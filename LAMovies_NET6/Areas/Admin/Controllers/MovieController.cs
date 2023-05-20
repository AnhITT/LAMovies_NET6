using LAMovies_NET6.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMoviesRepository _data;
        public MovieController(IMoviesRepository moviesRepository)
        {
            _data = moviesRepository;

        }
        public IActionResult QLMovies(string term = "", int currentPage = 1)
        {
            var movies = _data.List(term, true, currentPage);
            return View(movies);
        }
    }
}
