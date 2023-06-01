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

        public MoviesController(IMoviesRepository moviesRepository, IPricingRepository pricing, IGenreRepository genreRepository)
        {
            _movie = moviesRepository;
            _pricing = pricing;
            _genreRepository = genreRepository;
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

        [Authorize(Roles = "admin")]
        public IActionResult QLMovies(string term = "", int currentPage = 1)
        {
            var movies = _movie.List(term, true, currentPage);
            return View(movies);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddMovie()
        {
            var model = new Movie();
            model.GenreList = _genreRepository.List().Select(a => new SelectListItem { Text = a.nameGenre, Value = a.idGenre.ToString() });

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddMovie(Movie model)
        {
            model.GenreList = _genreRepository.List().Select(a => new SelectListItem { Text = a.nameGenre, Value = a.idGenre.ToString() });
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
            var model = _movie.GetById(id);
            var selectedGenres = _movie.GetGenreByMovieId(model.idMovie);
            MultiSelectList multiGenreList = new MultiSelectList(_genreRepository.List(), "idGenre", "nameGenre", selectedGenres);
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditMovie(Movie model)
        {
            var selectedGenres = _movie.GetGenreByMovieId(model.idMovie);
            MultiSelectList multiGenreList = new MultiSelectList(_genreRepository.List(), "idGenre", "nameGenre", selectedGenres);
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
    }
}
