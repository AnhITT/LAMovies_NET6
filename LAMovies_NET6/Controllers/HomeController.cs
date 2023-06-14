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
        private readonly IUserAuthRepository _userRepository;
        private readonly IActorRepository _actorRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IMoviesRepository moviesRepository, IPricingRepository pricing, IHttpContextAccessor httpContextAccessor,
            IUserAuthRepository userRepository, IActorRepository actorRepository)
        {
            _movie = moviesRepository;
            _pricing = pricing;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _actorRepository = actorRepository;
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
            ViewData["Title"] = "Dashboard";
            var top1Movie = _movie.Top1Movie();
            var countMovies = _movie.CountMovie();
            var countMoviesOdd = _movie.CountMovieOdd();
            var countMoviesSeries = _movie.CountMovieSeries();
            var countAccount = _userRepository.GetAllAccount().Count();
            var countAccountUseService = _pricing.GetAllUserPricings().Count();
            var countActor = _actorRepository.GetActorsList().Count();
            var revenue = _pricing.GetAllUserPricings().Sum(m => m.totalAmount);
            var topUserPricing = _pricing.Top1UserPricing();

            ViewBag.top1 = top1Movie.nameMovie;
            ViewBag.countMoviesSeries = countMoviesSeries;
            ViewBag.countMovies = countMovies;
            ViewBag.countMoviesOdd = countMoviesOdd;
            ViewBag.countAccount = countAccount;
            ViewBag.countAccountUseService = countAccountUseService;
            ViewBag.countActor = countActor;
            ViewBag.revenue = revenue;
            ViewBag.topUserPricing = topUserPricing.User.UserName;
            return View();
        }
    }
}