using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;

namespace LAMovies_NET6.Interfaces
{
    public interface IPricingRepository
    {
        IQueryable<Pricing> GetAllPricings();
        bool Add(Pricing model);
        bool Update(Pricing model);
        bool Delete(int id);
        Pricing GetById(int id);

        int CheckPricing();
        Task<Pricing> GetPricingById(int id);
        Task<List<UserPricingDTO>> GetPricingByUser();
        Task<UserPricing> CheckPricingExpired();
        List<UserPricing> GetAllUserPricings();
        UserPricing Top1UserPricing();

    }
}
