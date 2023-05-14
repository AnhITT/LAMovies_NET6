namespace LAMovies_NET6.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public int idMovie { get; set; }
        public int idGenre { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
