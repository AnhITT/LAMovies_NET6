namespace LAMovies_NET6.Models.DTO
{
    public class ListUserPricingDTO
    {
        public string usernameUser { get; set; }
        public string fullNameUser { get; set; }
        public string namePricing { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public TimeSpan remainingTime { get; set; }
    }
}
