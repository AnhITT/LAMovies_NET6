using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAMovies_NET6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesRepository _data;
        public HomeController(IMoviesRepository moviesRepository)
        {
            _data = moviesRepository;

        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _data.List(term, true, currentPage);
            return View(movies);
        }
        
    }
}