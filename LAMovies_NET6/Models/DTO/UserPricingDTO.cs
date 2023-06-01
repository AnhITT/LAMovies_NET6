namespace LAMovies_NET6.Models.DTO
{
    public class UserPricingDTO
    {
        public string namePricing { get; set; }
        public double pricePricing { get; set; }
        public int timePricing { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public TimeSpan remainingTime { get; set; }
    }
}
