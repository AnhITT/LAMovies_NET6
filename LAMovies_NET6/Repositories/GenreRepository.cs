using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.EntityFrameworkCore;

namespace LAMovies_NET6.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _data;
        public GenreRepository(ApplicationDbContext data) 
        {
            _data = data; 
        }
        public bool Add(Genre model)
        {
            try
            {
                _data.Genres.Add(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                _data.Genres.Remove(data);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            return _data.Genres.Find(id);
        }


        public bool Update(Genre model)
        {
            try
            {
                _data.Genres.Update(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Genre> GetGenresList()
        {
            return _data.Genres.ToList();
        }
        public ICollection<Movie> GetMoviesByGenres(int idGenres)
        {
            return _data.MovieGenres.Where(g => g.idGenre == idGenres).Select(m => m.Movie).ToList();
        }
    }
}
