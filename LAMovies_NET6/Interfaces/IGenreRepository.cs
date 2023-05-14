using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IGenreRepository
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        IQueryable<Genre> List();
    }
}
