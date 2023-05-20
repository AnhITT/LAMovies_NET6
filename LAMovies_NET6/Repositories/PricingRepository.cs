using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.EntityFrameworkCore;

namespace LAMovies_NET6.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private readonly ApplicationDbContext _data;
        public PricingRepository(ApplicationDbContext context) {
            _data = context;
        }

        public IQueryable<Pricing> GetAllPricings()
        {
            var data = _data.Pricings.AsQueryable();
            return data;
        }
    }
}
