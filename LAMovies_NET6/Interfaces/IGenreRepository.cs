using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IGenreRepository
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        List<Genre> GetGenresList();
        ICollection<Movie> GetMoviesByGenres(int idGenres);
        IQueryable<Genre> List();
        Genre GetGenreByName(string name);
    }
}
