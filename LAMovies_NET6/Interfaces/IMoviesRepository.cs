using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using System.Xml.Linq;

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
        List<int> GetActorByMovieId(int movieId);
        Movie GetDetailMovie(int idMovie);
        Movie WatchMovie(int idMovie);
        void UpdateView(Movie movie);
        List<Movie> ListMoviesUpdate();
        List<Movie> GetTop5MovieView();
        List<Movie> SortDate();
        Movie Top1Movie();
        List<Movie> MovieByGenre(int id);
        void SaveHistoryWatchedMovie(int id);
        void SaveHistoryWatchedMovieSeries(int id, int tap);
        List<Movie> HistoryMovieByUser();
        List<HistoryMoviesDTO> GetHistoryMovies(List<Movie> movies);
        bool AddOddMovie(OddMovie model);
        bool AddSeriesMovie(SeriesMovie model);
        OddMovie GetOddMovieById(int id);
        bool UpdateOddMovie(OddMovie model);
        List<SeriesMovie> GetSeriesById(int id);
        MovieListDTO ListSeriesMovies(bool paging = false, int currentPage = 0);
        MovieListDTO ListOddMovies(bool paging = false, int currentPage = 0);
        OddMovie GetURLOddMovie(int id);
        List<SeriesMovie> GetURLSeriesMovies(int id);

        MovieListDTO ListMoviesByCountry(string name, bool paging = false, int currentPage = 0);

        bool DeleteURLSeries(int id);
        Movie FindMovieByIdSeries(int idSeries);
        int CountMovie();
        int CountMovieOdd();
        int CountMovieSeries();
    }
}
