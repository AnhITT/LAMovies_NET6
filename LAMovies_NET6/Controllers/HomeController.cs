using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace LAMovies_NET6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesRepository _movie;
        private readonly IPricingRepository _pricing;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IMoviesRepository moviesRepository, IPricingRepository pricing, IHttpContextAccessor httpContextAccessor)
        {
            _movie = moviesRepository;
            _pricing = pricing;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _movie.List(term, true, currentPage);
            bool isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                var movieHistory = _movie.HistoryMovieByUser();
                if (movieHistory.Count == 0)
                {
                    ViewBag.movieHistory = null;
                }
                else
                {
                    ViewBag.movieHistory = movieHistory;
                }
                var movieNotify = _movie.GetHistoryMovies(movieHistory);
                HttpContext.Session.SetString("movieNotify", JsonConvert.SerializeObject(movieNotify));
            }
            ViewBag.listUpdate = _movie.ListMoviesUpdate();
            ViewBag.topViewMovie = _movie.GetTop5MovieView();
            ViewBag.sortMovie = _movie.SortDate();
            ViewBag.top1 = _movie.Top1Movie();
            return View(movies);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}