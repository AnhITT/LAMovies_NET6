using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LAMovies_NET6.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepository _actorRepository;
        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        [Authorize(Roles = "admin")]
        public IActionResult QLActor()
        {
            var genre = _actorRepository.GetActorsList();
            return View(genre);
        }
        [Authorize(Roles = "admin")]
        public IActionResult AddActor()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddActor(Actor model)
        {
            if (!ModelState.IsValid)
            {
                _actorRepository.Add(model);
                return Redirect("QLActor");
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View("add", model);
            }
        }
        [Authorize(Roles = "admin")]
        public IActionResult EditActor(int id)
        {
            var data = _actorRepository.GetById(id);
            return View(data);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditActor(Actor model)
        {
            if (!ModelState.IsValid)
            {
                _actorRepository.Update(model);
                return Redirect("../QLActor");
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        [Authorize(Roles = "admin")]
        public IActionResult DeleteActor(int id)
        {
            var result = _actorRepository.Delete(id);
            return RedirectToAction(nameof(QLActor));
        }
        public IActionResult ListMovieByActor(string name)
        {
            var item = _actorRepository.GetActorByName(name);
            var listMovie = _actorRepository.GetMoviesByActor(item.idActor);
            return View(listMovie);
        }
    }
}
