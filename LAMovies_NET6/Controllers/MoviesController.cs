using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LAMovies_NET6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _movie;
        private readonly IPricingRepository _pricing;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;
        public MoviesController(IMoviesRepository moviesRepository, IPricingRepository pricing, IGenreRepository genreRepository, IActorRepository actorRepository)
        {
            _movie = moviesRepository;
            _pricing = pricing;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
        }
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movie.GetDetailMovie(movieId);
            var moviesGenres = _movie.MovieByGenre(movieId);
            ViewBag.moviesGenres = moviesGenres;
            var movieHistory = _movie.HistoryMovieByUser();
            ViewBag.movieHistory = movieHistory;
            return View(movie);
        }
        public IActionResult ListMovies(string term = "", int currentPage = 1)
        {
            if(term == "")
            {
                ViewBag.search = null;
            }
            else
            {
                ViewBag.search = term;
            }
            var movies = _movie.List(term, true, currentPage);
            return View(movies);
        }

        public async Task<IActionResult> WatchMovie(int movieId)
        {
            var checkPricing = _pricing.CheckPricing();
            var pricingEx = await _pricing.CheckPricingExpired();
            ViewBag.CheckPricingExpired = pricingEx;
            if (checkPricing == 1)
            {
                    var url = _movie.GetURLOddMovie(movieId);
                    if(url != null)
                    {
                        ViewBag.url = url.urlMovie;
                    }
                    else
                    {
                        ViewBag.url = null;
                    }
                    var movie = _movie.WatchMovie(movieId);
                    _movie.UpdateView(movie);
                    _movie.SaveHistoryWatchedMovie(movieId);
                    var movieHistory = _movie.HistoryMovieByUser();
                    ViewBag.movieHistory = movieHistory;
                    return View(movie);
            }
            else if(checkPricing == 2)
            {
                return View("~/Views/Pricing/PricingEx.cshtml");
            }
            else
            {
                return View("~/Views/Pricing/PricingNone.cshtml");
            }
        }
        public async Task<IActionResult> WatchMovieSeries(int movieId, int tap)
        {
            var checkPricing = _pricing.CheckPricing();
            var pricingEx = await _pricing.CheckPricingExpired();
            ViewBag.CheckPricingExpired = pricingEx;
            if (checkPricing == 1)
            {
                var url = _movie.GetURLSeriesMovies(movieId);
                if (url.Count != 0)
                {
                    var movieLink = url.FirstOrDefault(m => m.practice == tap);
                    ViewBag.url = movieLink.urlMovie;
                    ViewBag.tap = movieLink.practice;
                    ViewBag.totalTap = _movie.GetById(movieId).episodes;
                    TempData["movieLink"] = url;
                }
                else
                {
                    ViewBag.url = null;
                }
                var movie = _movie.WatchMovie(movieId);
                _movie.UpdateView(movie);
                _movie.SaveHistoryWatchedMovieSeries(movieId, tap);
                var movieHistory = _movie.HistoryMovieByUser();
                ViewBag.movieHistory = movieHistory;
                return View(movie);
            }
            else if (checkPricing == 2)
            {
                return View("~/Views/Pricing/PricingEx.cshtml");
            }
            else
            {
                return View("~/Views/Pricing/PricingNone.cshtml");
            }
        }
       

        public IActionResult ListSeriesMovies(int currentPage = 1)
        {
            var movies = _movie.ListSeriesMovies(true, currentPage);
            return View(movies);
        }
        public IActionResult ListOddMovies(int currentPage = 1)
        {
            var movies = _movie.ListOddMovies(true, currentPage);
            return View(movies);
        }
        public IActionResult ListMoviesByCountry(string name, int currentPage = 1)
        {
            var movies = _movie.ListMoviesByCountry(name, true, currentPage);
            ViewBag.nameGenre = name;
            return View(movies);
        }
        [Authorize(Roles = "admin")]
        public IActionResult QLMovies(string term = "", int currentPage = 1)
        {
            ViewData["Title"] = "Quản lý phim";
            var movies = _movie.List(term, true, currentPage);
            return View(movies);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewData["Title"] = "Thêm mới phim";
            var model = new Movie();
            model.GenreList = _genreRepository.List().Select(a => new SelectListItem { Text = a.nameGenre, Value = a.idGenre.ToString() });
            model.ActorList = _actorRepository.List().Select(a => new SelectListItem { Text = a.nameActor, Value = a.idActor.ToString() });

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddMovie(Movie model)
        {
            model.GenreList = _genreRepository.List().Select(a => new SelectListItem { Text = a.nameGenre, Value = a.idGenre.ToString() });
            model.ActorList = _actorRepository.List().Select(a => new SelectListItem { Text = a.nameActor, Value = a.idActor.ToString() });
            var result = _movie.Add(model);
            if (result)
            { 
                return RedirectToAction(nameof(QLMovies));
            }
            else
            {
                TempData["msg"] = "Add Error";
                return View("AddMovie", model);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditMovie(int id)
        {
            ViewData["Title"] = "Chỉnh sửa phim";
            var model = _movie.GetById(id);
            var selectedGenres = _movie.GetGenreByMovieId(model.idMovie);
            var selectedActor = _movie.GetActorByMovieId(model.idMovie);

            MultiSelectList multiGenreList = new MultiSelectList(_genreRepository.List(), "idGenre", "nameGenre", selectedGenres);
            MultiSelectList multiActorList = new MultiSelectList(_actorRepository.List(), "idActor", "nameActor", selectedActor);

            model.MultiActorList = multiActorList;
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditMovie(Movie model)
        {
            var selectedGenres = _movie.GetGenreByMovieId(model.idMovie);
            var selectedActor = _movie.GetActorByMovieId(model.idMovie);

            MultiSelectList multiGenreList = new MultiSelectList(_genreRepository.List(), "idGenre", "nameGenre", selectedGenres);
            MultiSelectList multiActorList = new MultiSelectList(_actorRepository.List(), "idActor", "nameActor", selectedActor);

            model.MultiActorList = multiActorList;
            model.MultiGenreList = multiGenreList;
            var result = _movie.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(QLMovies));
            }
            else
            {
                TempData["msg"] = "Edit Error";
                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteMovie(int id)
        {
            var result = _movie.Delete(id);
            return RedirectToAction(nameof(QLMovies));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddURLOddMovie(int id)
        {
            var model = _movie.GetOddMovieById(id);
            ViewBag.idMovie = id;
            if(model != null)
            {
                ViewBag.idOdd = model.idOddMovie;
            }
            return View(model);
        }   
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddURLOddMovie(OddMovie model)
        {
            var result = _movie.AddOddMovie(model);
            if (result)
            {
                return RedirectToAction(nameof(QLMovies));
            }
            else
            {
                TempData["msg"] = "Add Error";
                return View("AddURLOddMovie", model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult UpdateURLOddMovie(OddMovie model)
        {
            var result = _movie.UpdateOddMovie(model);
            if (result)
            {
                return RedirectToAction(nameof(QLMovies));
            }
            else
            {
                TempData["msg"] = "Update Error";
                return View("AddURLOddMovie", model);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult QLURLSeriesMovie(int id)
        {
            var model = _movie.GetSeriesById(id);
            ViewBag.idMovie = id;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddURLSeriesMovie(int id)
        {
            ViewBag.idMovie = id;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddURLSeriesMovie(SeriesMovie model)
        {
            var result = _movie.AddSeriesMovie(model);
            if (result)
            {
                return RedirectToAction(nameof(QLURLSeriesMovie), new {id = model.idMovie});
            }
            else
            {
                TempData["msg"] = "Add Error";
                return View("AddURLOddMovie", model);
            }
        }
        [Authorize(Roles = "admin")]
        public IActionResult DeleteURLSeriesMovie(int id)
        {
            var movie = _movie.FindMovieByIdSeries(id);
            var result = _movie.DeleteURLSeries(id);
            return RedirectToAction(nameof(QLURLSeriesMovie), new { id = movie.idMovie });
        }
    }
}
