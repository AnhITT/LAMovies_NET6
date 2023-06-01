using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;

namespace LAMovies_NET6.Interfaces
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        bool Add(Movie model);
        bool Update(Movie model);
        Movie GetById(int id);
        bool Delete(int id);
        MovieListDTO List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetGenreByMovieId(int movieId);
        Movie GetDetailMovie(int idMovie);
        Movie WatchMovie(int idMovie);
        void UpdateView(Movie movie);
        List<Movie> ListMoviesUpdate();
        List<Movie> GetTop5MovieView();
        List<Movie> SortDate();
        Movie Top1Movie();
        List<Movie> MovieByGenre(int id);
        void SaveHistoryWatchedMovie(int id);
        List<Movie> HistoryMovieByUser();
        List<HistoryMoviesDTO> GetHistoryMovies(List<Movie> movies);

    }
}
