namespace LAMovies_NET6.Models
{
    public class MovieActor
    {
        public int idMovie { get; set; }
        public int idActor { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
