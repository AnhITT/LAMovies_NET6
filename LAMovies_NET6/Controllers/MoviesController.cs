using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LAMovies_NET6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _data;
        public MoviesController(IMoviesRepository moviesRepository)
        {
            _data = moviesRepository;
        }
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _data.GetDetailMovie(movieId);
            return View(movie);
        }
        public IActionResult WatchMovie(int movieId)
        {
            var movie = _data.WatchMovie(movieId);
            return View(movie);
        }
    }
}
