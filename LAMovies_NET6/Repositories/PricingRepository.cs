using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
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
        public int CheckPricing()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userPricing = _data.UserPricings.FirstOrDefault(
                                          up => up.idUser == userId &&
                                          up.startTime < DateTime.Now &&
                                          up.endTime > DateTime.Now);
            var userPricingEx = _data.UserPricings.FirstOrDefault(
                                          up => up.idUser == userId &&
                                          up.endTime < DateTime.Now);
            //TH1: Có
            if (userPricing != null)
            {
                return 1;
            }
            //TH2: Hết hạn
            else if (userPricingEx != null)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        public async Task<UserPricing> CheckPricingExpired()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _data.UserPricings.FirstOrDefault(
                                          up => up.idUser == userId &&
                                          up.endTime < DateTime.Now);
        }
        public async Task<List<UserPricingDTO>> GetPricingByUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listUserPricing = _data.UserPricings.Where(up => up.idUser == userId &&
                                          up.startTime < DateTime.Now &&
                                          up.endTime > DateTime.Now).ToList();
            List<UserPricingDTO> listPricing = new List<UserPricingDTO>();
            foreach ( var item in listUserPricing)
            {
                var pricing = _data.Pricings.FirstOrDefault(up => up.idPricing == item.idPricing);
                listPricing.Add(new UserPricingDTO()
                {
                    namePricing = pricing.namePricing,
                    pricePricing = pricing.pricePricing,
                    timePricing = pricing.timePricing,
                    startTime = item.startTime,
                    endTime = item.endTime,
                    remainingTime = item.endTime - DateTime.Now
                });
            }
            return listPricing;
        }
        public List<UserPricing> GetAllUserPricings()
        {
            var data = _data.UserPricings.ToList();
            return data;
        }

    }
}
