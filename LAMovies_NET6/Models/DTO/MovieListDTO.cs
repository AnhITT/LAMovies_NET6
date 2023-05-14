namespace LAMovies_NET6.Models.DTO
{
    public class MovieListDTO
    {
        public IQueryable<Movie> MovieList { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public string? term { get; set; }
    }
}
