using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class Pricing
    {
        [Key]
        public int idPricing { get; set; }
        public string namePricing { get; set; }
        public double pricePricing { get; set; }
        public int timePricing { get; set; }
     
        public ICollection<UserPricing> UserPricing { get; set; }

    }
}
