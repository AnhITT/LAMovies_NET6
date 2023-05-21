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
        private readonly IPricingRepository _pricing;
        public HomeController(IMoviesRepository moviesRepository, IPricingRepository pricing)
        {
            _movie = moviesRepository;
            _pricing = pricing;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _movie.List(term, true, currentPage);
            ViewBag.listUpdate = _movie.ListMoviesUpdate();
            ViewBag.topViewMovie = _movie.GetTop5MovieView();
            ViewBag.sortMovie = _movie.SortDate();
            ViewBag.top1 = _movie.Top1Movie();
            _pricing.CheckEndTime();
            return View(movies);
        }
    }
}