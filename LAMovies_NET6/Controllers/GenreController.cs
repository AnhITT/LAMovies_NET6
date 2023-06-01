using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LAMovies_NET6.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        [Authorize(Roles = "admin")]
        public IActionResult AddGenre()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddGenre(Genre model)
        {
            if (!ModelState.IsValid)
            {
                _genreRepository.Add(model);
                return Redirect("QLGenres");
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View("add", model);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditGenre(int id)
        {
            var data = _genreRepository.GetById(id);
            return View(data);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditGenre(Genre model)
        {
            if (!ModelState.IsValid)
            {
                _genreRepository.Update(model);
                return Redirect("../QLGenres");
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult QLGenres()
        {
            var genre = _genreRepository.GetGenresList();
            return View(genre);
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteGenre(int id)
        {
            var result = _genreRepository.Delete(id);
            return RedirectToAction(nameof(QLGenres));
        }
        [Authorize(Roles = "admin")]
        public IActionResult ShowList(int id)
        {
            var listMovie = _genreRepository.GetMoviesByGenres(id);
            var genre = _genreRepository.GetById(id);
            ViewBag.nameGenre = genre.nameGenre;
            return View(listMovie);
        }

        public IActionResult ListMovieByGenres(int id)
        {
            var listMovie = _genreRepository.GetMoviesByGenres(id);
            var genre = _genreRepository.GetById(id);
            ViewBag.nameGenre = genre.nameGenre;
            return View(listMovie);
        }


    }
}
