using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LAMovies_NET6.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _data;
        public MoviesRepository(ApplicationDbContext context)
        {
            _data = context;
        }

        public bool Add(Movie model)
        {
            try
            {
                _data.Movies.Add(model);
                _data.SaveChanges();
                AddMovieToGenre(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void AddMovieToGenre(Movie model)
        {
            foreach (int genreId in model.Genres)
            {
                var movieGenre = new MovieGenre
                {
                    idMovie = model.idMovie,
                    idGenre = genreId
                };
                _data.MovieGenres.Add(movieGenre);
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                DeleteMovieGenre(data);
                _data.Movies.Remove(data);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void DeleteMovieGenre(Movie model)
        {
            var movieGenres = _data.MovieGenres.Where(a => a.idMovie == model.idMovie);
            foreach (var movieGenre in movieGenres)
            {
                _data.MovieGenres.Remove(movieGenre);
            }
        }


        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _data.Movies.ToListAsync();
        }

        public Movie GetById(int id)
        {
            return _data.Movies.Find(id);
        }

        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = _data.MovieGenres.Where(a => a.idMovie == movieId).Select(a => a.idGenre).ToList();
            return genreIds;
        }

        public void DisplayGenresToMovie(List<Movie> list)
        {
            foreach (var movie in list)
            {
                var genres = (from genre in _data.Genres
                              join table in _data.MovieGenres
                              on genre.idGenre equals table.idGenre
                              where table.idMovie == movie.idMovie
                              select genre.nameGenre
                              ).ToList();
                movie.GenreNames = genres;
            }
        }

        public MovieListDTO List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new MovieListDTO();

            var list = _data.Movies.ToList();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.nameMovie.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 8;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.pageSize = pageSize;
                data.currentPage = currentPage;
                data.totalPages = TotalPages;
            }

            DisplayGenresToMovie(list);
            data.MovieList = list.AsQueryable();
            return data;
        }
        
        public Movie GetDetailMovie(int idMovie)
        {
            var movie = _data.Movies.Find(idMovie);
            var list = new List<Movie>();
            list.Add(movie);
            DisplayGenresToMovie(list);
            return movie;
        }
        public Movie WatchMovie(int idMovie)
        {
            var movie = _data.Movies.Find(idMovie);
            var list = new List<Movie>();
            list.Add(movie);
            DisplayGenresToMovie(list);
            return movie;
        }
        public bool Update(Movie model)
        {
            try 
            { 
            var genresToDeleted = _data.MovieGenres.Where(a => a.idMovie == model.idMovie && !model.Genres.Contains(a.idGenre)).ToList();
            foreach (var mGenre in genresToDeleted)
            {
                _data.MovieGenres.Remove(mGenre);
            }
            foreach (int genId in model.Genres)
            {
                var movieGenre = _data.MovieGenres.FirstOrDefault(a => a.idMovie == model.idMovie && a.idGenre == genId);
                if (movieGenre == null)
                {
                    movieGenre = new MovieGenre { idGenre = genId, idMovie = model.idMovie };
                    _data.MovieGenres.Add(movieGenre);
                }
            }
            _data.Movies.Update(model);
            _data.SaveChanges();
            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
