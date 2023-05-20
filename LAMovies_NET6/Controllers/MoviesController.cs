using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LAMovies_NET6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _movie;
        public MoviesController(IMoviesRepository moviesRepository)
        {
            _movie = moviesRepository;
        }
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movie.GetDetailMovie(movieId);
            return View(movie);
        }
        public IActionResult ListMovies(string term = "", int currentPage = 1)
        {
            var movies = _movie.List(term, true, currentPage);
            return View(movies);
        }

        public IActionResult WatchMovie(int movieId)
        {
            var movie = _movie.WatchMovie(movieId);
            _movie.UpdateView(movie);
            return View(movie);
        }
       
    }
}
