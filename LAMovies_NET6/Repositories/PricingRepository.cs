using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LAMovies_NET6.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private readonly ApplicationDbContext _data;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PricingRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _data = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Pricing> GetAllPricings()
        {
            var data = _data.Pricings.AsQueryable();
            return data;
        }
        public async Task<Pricing> GetPricingById(int id)
        {
            var pricing = await _data.Pricings.FindAsync(id);
            return pricing;
        }
        public UserPricing CheckPricing()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _data.UserPricings.FirstOrDefault(
                                          up => up.idUser == userId &&
                                          up.startTime < DateTime.Now &&
                                          up.endTime > DateTime.Now);
        }
        public void CheckEndTime()
        {
            DateTime currentTime = DateTime.Now;
            var checkEndtime = _data.UserPricings.Where(up => up.endTime < currentTime).ToList();
            foreach (var item in checkEndtime)
            {
                _data.UserPricings.Remove(item);
            }
            _data.SaveChanges();
        }
    }
}
