namespace LAMovies_NET6.Models.DTO
{
    public class HistoryMoviesDTO
    {
        public int idMovie { get; set; }
        public string nameMovie { get; set; }
        public string uriImg { get; set; }

        public TimeSpan remainingTime { get; set; }
    }
}
