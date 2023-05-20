using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_NET6.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreRepository.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _genreRepository.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreRepository.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult GenreList()
        {
            ViewBag.genresList = _genreRepository.GetGenresList();
            return PartialView("_PartialGenre", ViewBag.genresList);
        }

        public IActionResult Delete(int id)
        {
            var result = _genreRepository.Delete(id);
            return RedirectToAction(nameof(GenreList));
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
