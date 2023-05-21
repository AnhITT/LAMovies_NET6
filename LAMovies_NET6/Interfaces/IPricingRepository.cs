using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IPricingRepository
    {
        IQueryable<Pricing> GetAllPricings();
        UserPricing CheckPricing();
        void CheckEndTime();
        Task<Pricing> GetPricingById(int id);
    }
}
