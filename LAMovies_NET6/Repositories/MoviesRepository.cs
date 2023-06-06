using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace LAMovies_NET6.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _data;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MoviesRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _data = context;
            _httpContextAccessor = httpContextAccessor; 
        }

        public bool Add(Movie model)
        {
            try
            {
                model.viewMovie = 0;
                _data.Movies.Add(model);
                _data.SaveChanges();
                AddMovieToGenre(model);
                AddMovieToActor(model);
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
        public void AddMovieToActor(Movie model)
        {
            foreach (int actorId in model.Actor)
            {
                var movieActor = new MovieActor
                {
                    idMovie = model.idMovie,
                    idActor = actorId
                };
                _data.MovieActors.Add(movieActor);
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
            foreach (var movie in list)
            {
                var genres = (from actor in _data.Actors
                              join table in _data.MovieActors
                              on actor.idActor equals table.idActor
                              where table.idMovie == movie.idMovie
                              select actor.nameActor
                              ).ToList();
                movie.ActorNames = genres;
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
        public MovieListDTO ListMoviesByCountry(string name, bool paging = false, int currentPage = 0)
        {
            var data = new MovieListDTO();

            var list = _data.Movies.Where(m => m.subLanguageMovie == name).ToList();

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
            data.MovieList = list.AsQueryable();
            return data;
        }

        public MovieListDTO ListSeriesMovies(bool paging = false, int currentPage = 0)
        {
            var data = new MovieListDTO();

            var list = _data.Movies.Where(m => m.typeMovie == "seriesMovies").ToList();

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
            data.MovieList = list.AsQueryable();
            return data;
        }

        public MovieListDTO ListOddMovies(bool paging = false, int currentPage = 0)
        {
            var data = new MovieListDTO();

            var list = _data.Movies.Where(m => m.typeMovie == "oddMovies").ToList();

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

        public void UpdateView(Movie movie)
        {
            _data.Movies.Attach(movie);
            movie.viewMovie = movie.viewMovie + 1;
            _data.Entry(movie).Property(x => x.viewMovie).IsModified = true;
            _data.SaveChanges();
        }

        public List<Movie> ListMoviesUpdate()
        {
            var newestMovies = _data.Movies.OrderByDescending(p => p.yearCreateMovie).Take(3).ToList();
            DisplayGenresToMovie(newestMovies);
            return newestMovies;
        }

        public List<Movie> GetTop5MovieView()
        {
            var topMovies = _data.Movies.OrderByDescending(p => p.viewMovie).Take(5).ToList();
            return topMovies;
        }

        public List<Movie> SortDate()
        {
            var sortedMovies = _data.Movies.OrderByDescending(p => p.yearCreateMovie).ToList();
            return sortedMovies;
        }

        public Movie Top1Movie()
        {
            var topMovie = _data.Movies.OrderByDescending(p => p.viewMovie).FirstOrDefault();
            var list = new List<Movie>();
            list.Add(topMovie);
            DisplayGenresToMovie(list);
            return topMovie;
        }

        public List<Movie> MovieByGenre(int id)
        {
            List<MovieGenre> checkMovieGenres = _data.MovieGenres.Where(up => up.idMovie == id).ToList();
            List<MovieGenre> matchedGenres = _data.MovieGenres.Where(mg => checkMovieGenres.Select(m => m.idGenre).Contains(mg.idGenre)).ToList();
            List<Movie> movies = _data.Movies.Where(mov => matchedGenres.Select(m => m.idMovie).Contains(mov.idMovie)).ToList();
            return movies;
        }
        public void SaveHistoryWatchedMovie(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var checkIdMovie = _data.MovieHistorys.FirstOrDefault(m => m.idMovie == id && m.idUser == userId);
            if(checkIdMovie == null)
            {
                MovieHistory movieHistory = new MovieHistory()
                {
                    idMovie = id,
                    idUser = userId,
                    dateTimeWatch = DateTime.Now
                };
                _data.MovieHistorys.Add(movieHistory);
                _data.SaveChanges();
            }
            else
            {
                checkIdMovie.dateTimeWatch = DateTime.Now;
                _data.SaveChanges();
            }
        }
        public void SaveHistoryWatchedMovieSeries(int id, int tap)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var checkIdMovie = _data.MovieHistorys.FirstOrDefault(m => m.idMovie == id && m.idUser == userId);
            if (checkIdMovie == null)
            {
                MovieHistory movieHistory = new MovieHistory()
                {
                    idMovie = id,
                    idUser = userId,
                    episodes = tap,
                    dateTimeWatch = DateTime.Now
                };
                _data.MovieHistorys.Add(movieHistory);
                _data.SaveChanges();
            }
            else
            {
                checkIdMovie.episodes = tap;
                checkIdMovie.dateTimeWatch = DateTime.Now;
                _data.SaveChanges();
            }
        }
        public List<Movie> HistoryMovieByUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<MovieHistory> movieHistories = _data.MovieHistorys.Where(up => up.idUser == userId).ToList();
            var movies = from h in movieHistories
                         join m in _data.Movies on h.idMovie equals m.idMovie
                        orderby h.dateTimeWatch descending
                        select m;
            return movies.ToList();
        }
        public List<HistoryMoviesDTO> GetHistoryMovies(List<Movie> movies)
        {
            var list = new List<HistoryMoviesDTO>();
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            foreach (var item in movies)
            {
                MovieHistory movieCheck = _data.MovieHistorys.FirstOrDefault(m => m.idMovie == item.idMovie && m.idUser == userId);
                var movieHistory = new HistoryMoviesDTO()
                {
                    idMovie = item.idMovie,
                    nameMovie = item.nameMovie,
                    uriImg = item.uriImg,
                    typeMovie = item.typeMovie,
                    episodes = movieCheck.episodes,
                    remainingTime = DateTime.Now - movieCheck.dateTimeWatch
                };
                list.Add(movieHistory);
            }
            return list;
        }
        public bool AddOddMovie(OddMovie model)
        {
            try
            {
                _data.OddMovies.Add(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public OddMovie GetOddMovieById(int id)
        {
            return _data.OddMovies.FirstOrDefault(u => u.idMovie == id);
        }
        public bool UpdateOddMovie(OddMovie model)
        {
            try
            {
                _data.OddMovies.Update(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SeriesMovie> GetSeriesById(int id)
        {
            return _data.SeriesMovies.Where(u => u.idMovie == id).ToList();
        }

        public bool AddSeriesMovie(SeriesMovie model)
        {
            try
            {
                _data.SeriesMovies.Add(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public OddMovie GetURLOddMovie(int id)
        {
            var item = _data.OddMovies.FirstOrDefault(m => m.idMovie == id);
            return item;
        }

        public List<SeriesMovie> GetURLSeriesMovies(int id)
        {
            var item = _data.SeriesMovies.Where(m => m.idMovie == id).ToList();
            return item;
        }
        public bool DeleteURLSeries(int id)
        {
            try
            {
                var data = _data.SeriesMovies.Find(id);
                if (data == null)
                    return false;
                _data.SeriesMovies.Remove(data);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Movie FindMovieByIdSeries(int idSeries)
        {
            var series = _data.SeriesMovies.FirstOrDefault(m => m.idSeries == idSeries);
            var movie = _data.Movies.FirstOrDefault(m => m.idMovie == series.idMovie);
            return movie;
        }
    }
}
