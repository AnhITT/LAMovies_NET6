using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAMovies_NET6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesRepository _movie;
        public HomeController(IMoviesRepository moviesRepository)
        {
            _movie = moviesRepository;

        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _movie.List(term, true, currentPage);
            return View(movies);
        }
        public IActionResult _PartialNewMovies()
        {
            var movie = _movie.ListMoviesUpdate();
            return PartialView(movie);
        }
    }
}