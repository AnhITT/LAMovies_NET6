using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;
        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListProduct()
        {
            IEnumerable<Movie> movies = await _moviesRepository.GetAllMovies();
            return PartialView("_ListProduct", movies);
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            Movie movie = await _moviesRepository.GetMovieById(id);
            return View(movie);
        }
        public IActionResult MovieDetail()
        {
            return View();
        }
    }
}
