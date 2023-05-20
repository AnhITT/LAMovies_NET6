using LAMovies_NET6.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LAMovies_NET6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;

        }
       
        
    }
}
