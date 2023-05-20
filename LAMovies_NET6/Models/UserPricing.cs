namespace LAMovies_NET6.Models
{
    public class UserPricing
    {
        public string idUser { get; set; }
        public int idPricing { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public Pricing Pricing { get; set; }
        public User User { get; set; }
    }
}
