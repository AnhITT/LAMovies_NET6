using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.EntityFrameworkCore;

namespace LAMovies_NET6.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;
        public MoviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(i => i.idMovie == id);
        }
    }
}
