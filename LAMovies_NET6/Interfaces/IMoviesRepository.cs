using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);
    }
}
