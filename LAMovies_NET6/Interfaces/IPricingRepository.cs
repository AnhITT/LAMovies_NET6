using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IPricingRepository
    {
        IQueryable<Pricing> GetAllPricings();
    }
}
