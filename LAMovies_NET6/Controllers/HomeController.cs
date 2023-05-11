using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAMovies_NET6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;

        }



        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}