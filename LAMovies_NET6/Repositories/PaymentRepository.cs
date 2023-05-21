using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using System.Security.Claims;

namespace LAMovies_NET6.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _data;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public PaymentRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _data = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public void SaveDataService(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var pricing = _data.Pricings.FirstOrDefault(p => p.idPricing == id);
            UserPricing userPricing = new UserPricing
            {
                idPricing = id,
                idUser = userId,
                startTime = DateTime.Now,
                endTime = DateTime.Now.AddMonths(pricing.timePricing),
            };
            _data.Add(userPricing);
            _data.SaveChanges();
        }
        
    }
}
